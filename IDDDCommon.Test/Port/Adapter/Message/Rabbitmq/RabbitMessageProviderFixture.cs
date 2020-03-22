using IDDDCommon.Port.Adapter.Messaging.Rabbitmq;
using NUnit.Framework;

namespace IDDDCommon.Test.Port.Adapter.Message.Rabbitmq
{
    [TestFixture]
    public class RabbitMessageProviderFixture
    {
        [Test]
        public void CanSendMessage()
        {
            var message = "Test-Message";
            new RabbitMQMessageProducer().Send(message);
        }

        [Test]
        public void CanReceiveMessage()
        {
            new RabbitMQMessageListener().Receive();
        }
    }
}