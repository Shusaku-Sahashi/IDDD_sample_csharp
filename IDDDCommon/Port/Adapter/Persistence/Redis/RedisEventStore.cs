using System.Collections.Generic;
using IDDDCommon.Event.Source;
using ServiceStack.Redis;

namespace IDDDCommon.Port.Adapter.Persistence.Redis
{
    public class RedisEventStore : EventStore
    {
        public IEnumerable<StoredEvent> AllStoredEventSince(long @from)
        {
            using var redisManager = new PooledRedisClientManager();
            using var redis = redisManager.GetClient();

            var redisStoredEvent =  redis.As<StoredEvent>();
            var sequenceEnd = redis.Get<long>(redisStoredEvent.SequenceKey);

            var storedEvents = new List<StoredEvent>();
            
            for (var idSequence = @from; idSequence  < sequenceEnd; idSequence++)
            {
                storedEvents.Add(redisStoredEvent.GetById(idSequence));
            }
            
            return storedEvents;
        }

        public void Store(StoredEvent storedEvent)
        {
            using var redisManager = new PooledRedisClientManager();
            using var redis = redisManager.GetClient();

            var redisStoredEvent =  redis.As<StoredEvent>();

            storedEvent.Id = redisStoredEvent.GetNextSequence();

            redisStoredEvent.Store(storedEvent);
        }

        public void Purge()
        {
            using var redisManager = new PooledRedisClientManager();
            using var redis = redisManager.GetClient();

            var redisStoredEvent = redis.As<StoredEvent>();
            
            redisStoredEvent.SetSequence(0);
            
            redisStoredEvent.DeleteAll();
        }
    }
}