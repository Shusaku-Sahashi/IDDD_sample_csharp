using System;
using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public abstract class BrokerChannel : IDisposable
    {
        public IModel Channel { get; protected set; }
        private IConnection Connection { get; set; }
        public string Name { get; protected set; }
        public string ExchangeName { get; protected set; }

        protected BrokerChannel()
        {
            var factory = new ConnectionFactory
            {
                HostName = ConnectionSettings.Instance().HostName,
                Port = ConnectionSettings.Instance().Port,
                VirtualHost = ConnectionSettings.Instance().VirtualHost,
            };

            this.Connection = factory.CreateConnection();
            this.Channel = this.Connection.CreateModel();
        }

        public void Dispose()
        {
            Channel?.Dispose();
            Connection?.Dispose();
        }
    }
}