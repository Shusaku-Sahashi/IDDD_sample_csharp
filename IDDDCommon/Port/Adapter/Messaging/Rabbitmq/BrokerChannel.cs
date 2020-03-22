using System;
using System.Threading.Channels;
using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    /// <summary>
    /// MessageBrokerのChannel
    /// </summary>
    public abstract class BrokerChannel
    {
        protected IModel Channel { get; set; }
        protected IConnection Connection { get; set; }
        
        internal string Host { get; set; }
        internal string Name { get; set; }
        
        /// <summary>
        /// ConnectionSettingを指定されて生成された場合
        /// </summary>
        /// <param name="connectionSettings"></param>
        /// <param name="name"></param>
        protected BrokerChannel(
            ConnectionSettings connectionSettings,
            string name)
        {
            var factory = ConfigureConnectionFactoryUsing(connectionSettings);
            this.Connection = factory.CreateConnection();
            this.Channel = this.Connection.CreateModel();
        }

        /// <summary>
        /// BrokerChannelを継承したクラスから生成する場合
        /// 
        /// EX.) Exchangeと同じChannel, ConnectionでQueueを作成
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="name"></param>
        protected BrokerChannel(BrokerChannel channel)
        {
            this.Host = channel.Host;
            this.Channel = channel.Channel;
            this.Connection = channel.Connection;
        }

        public ChannelToken ChannelToken()
        {
            return new ChannelToken(Channel, Connection);
        }

        private static ConnectionFactory ConfigureConnectionFactoryUsing(ConnectionSettings connectionSettings)
        {
            var factory = new ConnectionFactory()
            {
                HostName = connectionSettings.HostName,
                Port = connectionSettings.Port,
                VirtualHost = connectionSettings.VirtualHost,
            };
            if (connectionSettings.Username != null) factory.UserName = connectionSettings.Username;
            if (connectionSettings.Password != null) factory.UserName = connectionSettings.Password;

            return factory;
        }
    }

    public class ChannelToken : IDisposable
    {
        public IModel Channel { get;}
        public IConnection Connection { get; }
        
        public ChannelToken(IModel channel, IConnection connection)
        {
            Channel = channel;
            Connection = connection;
        }
        
        public void Dispose()
        {
            Channel.Dispose();
            Connection.Dispose();
        }
    }
}