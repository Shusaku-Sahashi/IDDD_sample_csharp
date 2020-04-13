using System;
using IDDDCommon.Domain.Model.Process;
using IDDDCommon.Event.Source;
using IDDDCommon.Notification;

namespace IDDDAgilePm.Application
{
    internal class ApplicationServiceLifeCycle
    {
        private EventStore _eventStore;
        private NotificationPublisher _notificationPublisher;
        private IPublishedNotificationTrackerStore _publishedNotificationTrackerStore;

        public ApplicationServiceLifeCycle(EventStore eventStore, NotificationPublisher notificationPublisher,
            IPublishedNotificationTrackerStore publishedNotificationTrackerStore)
        {
            this._eventStore = eventStore;
            this._notificationPublisher = notificationPublisher;
            this._publishedNotificationTrackerStore = publishedNotificationTrackerStore;
        }

        public IDisposable Begin()
        {
            return this.Listen();
        }

        private IDisposable Listen()
        {
            return DomainEventPublisher.Current().Register<DomainEvent>(domainEvent =>
            {
                this._eventStore.Store(domainEvent);
            });
        }
    }
}