using IDDDCommon.Config;
using Microsoft.Extensions.Configuration;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class ConnectionSettings
    {
        public string HostName { get; private set; }
        public int Port { get; private set; }
        public string VirtualHost { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        
        public static ConnectionSettings Instance()
        {
            var configSections = ApplicationConfig.Current()
                .GetSection("RabbitMQ").Get<ConnectionSettingsDTO>();
            
            return new ConnectionSettings(
                configSections.HostName,
                configSections.Port,
                configSections.VirtualHost,
                configSections.Username,
                configSections.Password);
        }

        private ConnectionSettings(string hostname, int port, string virtualHost, string username, string password)
        {
            this.HostName = hostname;
            this.Port = port;
            this.VirtualHost = virtualHost;
            this.Username = username;
            this.Password = password;
        }
    }
    
    internal class ConnectionSettingsDTO
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}