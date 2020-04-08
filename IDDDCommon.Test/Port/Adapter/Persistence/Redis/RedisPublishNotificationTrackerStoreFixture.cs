
using System.Collections.Generic;
using IDDDCommon.Notification;
using IDDDCommon.Port.Adapter.Persistence.Redis;
using IDDDCommon.Test.Port.Adapter.Messaging.Rabbitmq;
using NUnit.Framework;

namespace IDDDCommon.Test.Port.Adapter.Persistence.Redis
{
    [TestFixture]
    public class RedisPublishNotificationTrackerStoreFixture
    {
        [SetUp]
        public void SetUp()
        {
            var store = new RedisPublishNotificationTrackerStore();
            store.Purge();
        }
        [Test]
        public void CanTrack()
        {
            // Arrange
            var store = new RedisPublishNotificationTrackerStore();
            var tracker = new PublishedNotificationTracker
            {
                CurrencyVersion = 1,
                TypeName = "test-type-name",
                MostLatestPublishedId = 1,
            };

            var notifications = new List<Notification.Notification>
            {
                new Notification.Notification(1, new MyDomainEvent("test-message-1")),
                new Notification.Notification(2, new MyDomainEvent("test-message-2")),
                new Notification.Notification(3, new MyDomainEvent("test-message-3")),
            };
            
            // Action
            store.TrackMotsResentPublishedNotification(tracker, notifications);
            
            // Assert
            var mostLatestTracker = store.PublishedNotificationTracker();
            Assert.That(mostLatestTracker.MostLatestPublishedId, Is.EqualTo(3));
        }
    }
}