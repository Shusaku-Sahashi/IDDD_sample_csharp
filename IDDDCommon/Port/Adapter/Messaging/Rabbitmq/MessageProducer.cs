using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class MessageProducer
    {
        private readonly string channelName;
        
        public MessageProducer(string channelName)
        {
            this.channelName = channelName;
        }
        
        public void Send(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = ConnectionSettings.Instance().HostName,
                Port = ConnectionSettings.Instance().Port,
                VirtualHost = ConnectionSettings.Instance().VirtualHost,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(channelName, ExchangeType.Fanout);
            
            channel.BasicPublish(channelName, 
                "", 
                null, 
                Encoding.UTF8.GetBytes(message));
        }
    }
}