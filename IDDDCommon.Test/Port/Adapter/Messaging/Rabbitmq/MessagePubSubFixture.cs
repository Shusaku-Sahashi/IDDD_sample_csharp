using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IDDDCommon.Domain.Model.Process;
using IDDDCommon.Notification;
using IDDDCommon.Port.Adapter.Messaging;
using IDDDCommon.Port.Adapter.Messaging.Rabbitmq;
using NUnit.Framework;

namespace IDDDCommon.Test.Port.Adapter.Messaging.Rabbitmq
{
    [TestFixture]
    public class MessageProducerFixture
    {
        internal static readonly Dictionary<string, string> NotificationMap = new Dictionary<string, string>();

        [SetUp]
        public void SetUp()
        {
            var myExchangeListener = new MyExchangeListener();
        }
            
        [Test]
        public async Task CanSendMessage()
        {
            const string exchangeMessage = "test-message";
            //Arrange
            var exchange = new Exchange(Exchanges.AgilePm);
            var message =
                NotificationSerializer.Serialize(
                    new IDDDCommon.Notification.Notification(1L, new MyDomainEvent(exchangeMessage)));

            // Action
            new MessageProducer(exchange).Send(message);
            
            // 送信してから少し待つ。
            await Task.Delay(3000);
            
            // Assert
            Assert.That(NotificationMap[Exchanges.AgilePm.ToString()], Is.EqualTo(exchangeMessage));
        }
    }
    public class MyDomainEvent : DomainEvent
    {
        private const int DomainEventVersion = 1;
        public int EventVersion { get; }
        public string EventId { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public string Message { get; private set; }

        public MyDomainEvent(string message)
        {
            this.EventVersion = DomainEventVersion;
            this.EventId = "88b0543c-e17b-4edb-a7c5-4ce8f0d8bb36";
            this.OccurredOn = DateTime.Parse("2020-03-28T19:04:53.191488+09:00");
            this.Message = message;
        }
    }

    public class MyExchangeListener : ExchangeListener
    {
        public override void FilterDispatch(string message)
        {
            using var reader = new NotificationReader(message);
            var notificationMessage = reader.StringEventValue(nameof(MyDomainEvent.Message));
            MessageProducerFixture.NotificationMap.Add(Exchanges.AgilePm.ToString(), notificationMessage);
        }

        protected override Exchanges ExchangeName() => Exchanges.AgilePm;
    }
}