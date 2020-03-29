using System;
using System.Text;
using IDDDCommon.Port.Adapter.Messaging.Rabbitmq;
using RabbitMQ.Client;

namespace IDDDAgilePm
{
    class Program
    {
        static void Main(string[] args)
        {
            // new RabbitMQMessageProducer()._Send("fanout-test");
            var factory = new ConnectionFactory()
            {
                HostName = ConnectionSettings.Instance().HostName,
                Port = ConnectionSettings.Instance().Port,
                VirtualHost = ConnectionSettings.Instance().VirtualHost,
            };
            
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var exchangeName = "MessageProducerFixtureExchange";
//            var queueName = "MessageProducerFixtureQueue";
            channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
//            channel.QueueDeclare(queueName, false, false, false, null);
//            channel.QueueBind(queueName, exchangeName, "", null);
            
            channel.BasicPublish(exchangeName, "", null, Encoding.UTF8.GetBytes("test-message"));
        }
    }
}