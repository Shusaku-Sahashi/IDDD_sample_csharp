using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using IDDDCommon.Domain.Model.Process;

namespace IDDDCommon.Notification
{
    /// <summary>
    /// DomainEventを外部送信する場合に使用するデータ構造
    /// </summary>
    public class Notification
    {
        private const int NotificationVersion = 1;
        public long NotificationId { get; private set; }
        [JsonIgnore]
        public DomainEvent DomainEvent { get; private set; }
        
        public string TypeName { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public int Version { get; private set; }

        // System.Text.Jsonで派生クラスをフィールドに持つ場合、派生クラス側のフィールドがJsonSerializeされないので、それを回避する方法。
        // https://qiita.com/mxProject/items/145e35315daf0d072254
        [JsonPropertyName(nameof(Notification.DomainEvent))]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public object DomainEventObject
        {
            get => this.DomainEvent;
            set => DomainEvent = (DomainEvent) value;
        }

        public Notification(long notificationId, DomainEvent domainEvent)
        {
            this.NotificationId = notificationId;
            this.DomainEvent = domainEvent;
            this.TypeName = domainEvent.GetType().FullName + ", " + domainEvent.GetType().Assembly.GetName().Name;
            this.OccurredOn = domainEvent.OccurredOn;
            this.Version = NotificationVersion;
        }
    }
}