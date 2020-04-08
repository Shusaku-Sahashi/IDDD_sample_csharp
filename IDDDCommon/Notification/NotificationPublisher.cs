using System;
using System.Collections.Generic;
using System.Linq;
using IDDDCommon.Domain.Model.Process;
using IDDDCommon.Event.Source;
using IDDDCommon.Port.Adapter.Messaging.Rabbitmq;

namespace IDDDCommon.Notification
{
    public class NotificationPublisher
    {
        public NotificationPublisher(IPublishedNotificationTrackerStore publishedNotificationTrackerStore,
            EventStore eventStore, MessageProducer exchangeProducer)
        {
            this.PublishedNotificationTrackerStore = publishedNotificationTrackerStore;
            this.EventStore = eventStore;
            this.MessageProducer = exchangeProducer;
        }

        /// <summary>
        /// 現在EventStoreに蓄積されているStoredEventをRabbitMQを利用して全て送信する。
        /// </summary>
        public void PublishNotification()
        {
            // Notificationが発行された最新のIDを取得する。
            var publishedNotificationTracker =
                this.PublishedNotificationTrackerStore.PublishedNotificationTracker();
            
            // 最新の未送信Listを作成
            var unpublishedNotifications = this.ListUnpublishedNotifications(
                publishedNotificationTracker.MostLatestPublishedId).ToList();
            
            this.Publish(unpublishedNotifications);

            // 送信したNotificationを直近の送信履歴として保存
            this.PublishedNotificationTrackerStore.
                TrackMotsResentPublishedNotification(publishedNotificationTracker, unpublishedNotifications);
        }

        private IEnumerable<Notification> ListUnpublishedNotifications(long mostPublishedNotificationId)
        {
            // 取得したIDから直近のDomainEventをEventStoreから取得する。
            var unpublishedDomainEvents = this.EventStore.AllStoredEventSince(mostPublishedNotificationId);
            // 取得したEventStoreをNotificationに変換する。
            return NotificationFrom(unpublishedDomainEvents);
        }

        private static IEnumerable<Notification> NotificationFrom(IEnumerable<StoredEvent> storedEvents)
        {
            return storedEvents.Select(storedEvent => 
                new Notification(storedEvent.Id, storedEvent.ToDomainEvent<DomainEvent>()));
        }

        private void Publish(IEnumerable<Notification> notifications)
        {
            // 取得したEventStoreをNotificationにSerializeする。
            var serializeNotifications = notifications.Select(NotificationSerializer.Serialize);
            // SerializeしたNotification, 付属属性をRabbitMQを用いて送信する。
            foreach (var notification in serializeNotifications)
            {
                this.MessageProducer.Send(notification);
            }
        }

        private IPublishedNotificationTrackerStore PublishedNotificationTrackerStore { get;  set; }
        private EventStore EventStore { get; set; }
        private MessageProducer MessageProducer { get; set; }
    }
}