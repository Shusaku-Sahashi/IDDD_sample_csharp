using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using IDDDCommon.Media;

namespace IDDDCommon.Notification
{
    internal class NotificationReader : AbstractJsonMediaReader
    {
        private JsonElement Event { get; set; }
        public NotificationReader(string message) : base(message)
        {
            this.Event = this.ElementOf(nameof(Notification.DomainEvent));
        }
        public string StringEventValue(params string[] keys) => this.Event.StringValue(keys);
        public float? FloatEventValue(params string[] keys) => this.Event.FloatValue(keys);
        public long? LongEventValue(params string[] keys) => this.Event.LongValue(keys);
        public DateTime? DateTimeEventValue(params string[] keys) => this.Event.DateTimeValue(keys);
        public long? NotificationId() => this.LongValue(nameof(Notification.NotificationId));
        public DateTime? OccurredOn() => this.DateTimeValue(nameof(Notification.OccurredOn));
        public string TypeName() => this.StringValue(nameof(Notification.TypeName));
        public string Version() => this.StringValue(nameof(Notification.Version));
    }
}