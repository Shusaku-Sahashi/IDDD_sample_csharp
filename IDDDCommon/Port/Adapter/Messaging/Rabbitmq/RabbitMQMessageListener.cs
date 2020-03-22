using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class RabbitMQMessageListener
    {
        public void Receive()
        {
            var factory = new ConnectionFactory()
            {
                HostName = ConnectionSettings.Instance().HostName,
                VirtualHost = ConnectionSettings.Instance().VirtualHost,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("hello", false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received {0}", message);
            };

            channel.BasicConsume("hello", true, consumer);
        }
    }
}