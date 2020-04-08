using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using IDDDCommon.Notification;
using ServiceStack.Redis;

namespace IDDDCommon.Port.Adapter.Persistence.Redis
{
    public class RedisPublishNotificationTrackerStore : IPublishedNotificationTrackerStore
    {
        private const string PrimaryKey = "PUBNOTIF_TRACKER#PK";
        
        public PublishedNotificationTracker PublishedNotificationTracker()
        {
            using var redisManager = new PooledRedisClientManager();
            using var redis = redisManager.GetClient();

            var redisClient = redis.As<PublishedNotificationTracker>();
            return redisClient.GetValue(PrimaryKey);
        }

        public void TrackMotsResentPublishedNotification(PublishedNotificationTracker publishedNotificationTracker,
            IEnumerable<Notification.Notification> notifications)
        {
            using var redisManager = new PooledRedisClientManager();
            using var redis = redisManager.GetClient();
            
            var redisClient = redis.As<PublishedNotificationTracker>();
            var latestId = notifications.OrderByDescending(n => n.NotificationId).First().NotificationId;
            publishedNotificationTracker.MostLatestPublishedId = latestId;
            
            redisClient.SetValue(PrimaryKey, publishedNotificationTracker);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Purge()
        {
            using var redisManager = new PooledRedisClientManager();
            using var redis = redisManager.GetClient();
            
            redis.FlushAll();
        }
    }
}