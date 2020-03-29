using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class Queue : BrokerChannel
    {
        public Queue(string name, Exchange exchange)
        {
            this.Channel = CreateChannel(name, exchange);
            this.ExchangeName = exchange.Name;
        }
        
        private static IModel CreateChannel(string queueName, Exchange exchange)
        {
            var factory = new ConnectionFactory()
            {
                HostName = ConnectionSettings.Instance().HostName,
                Port = ConnectionSettings.Instance().Port,
                VirtualHost = ConnectionSettings.Instance().VirtualHost,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queueName, false, false, false, null);
            channel.ExchangeBind(queueName, exchange.Name, "");

            return channel;
        }
    }
}