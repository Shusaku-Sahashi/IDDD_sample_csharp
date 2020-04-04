using System;
using RabbitMQ.Client;
using ServiceStack.Redis;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public abstract class ExchangeListener 
    {
        protected ExchangeListener()
        {
            var queue = AttachedQueue();
            RegisterConsumer(queue);
        }
        
        public abstract void FilterDispatch(string message);
        
        protected abstract Exchanges ExchangeName();
        
        private string QueueName() => this.GetType().Name;
        
        private Queue AttachedQueue()
        {
            var exchange = new Exchange(this.ExchangeName());
            var queue = new Queue(this.ExchangeName() + "." + this.QueueName(), exchange);

            return queue;
        }

        private void RegisterConsumer(Queue queue)
        {
            var consumer = new MessageConsumer(queue);
            consumer.ReceiveFor(new ExchangeMessageListener(this));
        }
    }

    public class ExchangeMessageListener : IMessageListener
    {
        private readonly ExchangeListener _exchangeListener;
        public ExchangeMessageListener(ExchangeListener exchangeListener)
        {
            this._exchangeListener = exchangeListener;
        }

        public void HandleMessage(string type, string messageId, DateTime timestamp, string message, ulong deliveryTag,
            bool isRedelivery)
        {
            _exchangeListener.FilterDispatch(message);
        }
    }
}