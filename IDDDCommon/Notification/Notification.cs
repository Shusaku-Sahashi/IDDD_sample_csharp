using System;
using IDDDCommon.Domain.Model.Process;
using Newtonsoft.Json;

namespace IDDDCommon.Notification
{
    /// <summary>
    /// DomainEventを外部送信する場合に使用するデータ構造
    /// </summary>
    public class Notification
    {
        [JsonIgnore]
        private int _notivicationVersion = 1;
        public long NotificationId { get; private set; }
        public DomainEvent DomainEvent { get; private set; }
        public string TypeName { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public int Version { get; private set; }

        public Notification(long notificationId, DomainEvent domainEvent)
        {
            this.NotificationId = notificationId;
            this.DomainEvent = domainEvent;
            this.TypeName = domainEvent.GetType().FullName + ", " + domainEvent.GetType().Assembly.GetName().Name;
            this.OccurredOn = domainEvent.OccurredOn;
            this.Version = _notivicationVersion;
        }
    }
}