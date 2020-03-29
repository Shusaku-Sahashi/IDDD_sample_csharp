using System;
using IDDDCommon.Domain.Model.Process;
using IDDDCommon.Port.Adapter.Messaging.Rabbitmq;
using IDDDCommon.Test.Notification;
using NUnit.Framework;

namespace IDDDCommon.Test.Port.Adapter.Message.Rabbitmq
{
    [TestFixture]
    public class MessageProducerFixture
    {
        [Test]
        public void CanSendMessage()
        {
            /*Arrange*/
            using var exchange = new Exchange($"{nameof(MessageProducerFixture)}Exchange");
            var message =
                NotificationSerializer.Serialize(
                    new IDDDCommon.Notification.Notification(1L, new MyDomainEvent("test-message")));

            /*Action*/
            new MessageProducer(exchange).Send(message);
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