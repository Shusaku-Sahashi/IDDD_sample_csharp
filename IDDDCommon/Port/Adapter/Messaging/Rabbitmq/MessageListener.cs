using System;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public interface IMessageListener
    {
        void HandleMessage(
            string type,
            string messageId,
            DateTime timestamp,
            string message,
            ulong deliveryTag,
            bool isRedelivery);
    }
}