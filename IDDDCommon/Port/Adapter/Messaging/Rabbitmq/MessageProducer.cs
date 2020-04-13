using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class MessageProducer
    {
        private readonly Exchange _exchange;
        
        public MessageProducer(Exchange exchange)
        {
            this._exchange = exchange;
        }
        
        public void Send(string message)
        {
            this._exchange.Channel.BasicPublish(exchange: this._exchange.ExchangeName,
                "", 
                null, 
                Encoding.UTF8.GetBytes(message));
        }
    }
}