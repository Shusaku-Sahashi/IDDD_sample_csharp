using System;
using IDDDCommon.Domain.Model.Process;
using IDDDCommon.Notification;
using IDDDCommon.Test.Notification;
using NUnit.Framework;

namespace IDDDCommon.Test
{
    [TestFixture]
    public class NotificationSerializerFixture
    {
        private DomainEvent myDomainEvent = new MyDomainEvent("serialize-test");

        [Test]
        public void CanSerializeNotification()
        {
            var notification = new IDDDCommon.Notification.Notification(1, myDomainEvent);
            var actual = NotificationSerializer.Serialize(notification);

            const string expected =
                "{\"NotificationId\":1,\"DomainEvent\":{\"EventVersion\":1,\"EventId\":\"88b0543c-e17b-4edb-a7c5-4ce8f0d8bb36\",\"OccurredOn\":\"2020-03-28T19:04:53.191488+09:00\",\"Message\":\"serialize-test\"},\"TypeName\":\"IDDDCommon.Test.NotificationSerializerFixture+MyDomainEvent, IDDDCommon.Test\",\"OccurredOn\":\"2020-03-28T19:04:53.191488+09:00\",\"Version\":1}";

            Assert.That(actual, Is.EqualTo(expected));
        }

        private class MyDomainEvent : DomainEvent
        {
            private const int _domainEventVersion = 1;

            public MyDomainEvent(string message)
            {
                this.EventVersion = _domainEventVersion;
                this.EventId = "88b0543c-e17b-4edb-a7c5-4ce8f0d8bb36";
                this.OccurredOn = DateTime.Parse("2020-03-28T19:04:53.191488+09:00");
                this.Message = message;
            }

            public int EventVersion { get; }
            public string EventId { get; private set; }
            public DateTime OccurredOn { get; private set; }
            public string Message { get; private set; }
        }
    }
}