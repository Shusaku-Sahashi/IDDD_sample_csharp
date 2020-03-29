using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class MessageProducer
    {
        private readonly BrokerChannel _channel;
        
        public MessageProducer(BrokerChannel channel)
        {
            this._channel = channel;
        }
        
        public void Send(string message)
        {
            this._channel.Channel.BasicPublish(exchange: this._channel.ExchangeName,
                "", 
                null, 
                Encoding.UTF8.GetBytes(message));
        }
    }
}