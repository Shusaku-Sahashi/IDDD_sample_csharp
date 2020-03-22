using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Transactions;
using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class Exchanger : BrokerChannel
    {
        public string Type { get; private set; }
        
        public static Exchanger FanOutInstance(ConnectionSettings connectionSettings, string name)
        {
            return new Exchanger(connectionSettings, name, ExchangeType.Fanout);
        }
        
        public Exchanger(ConnectionSettings connectionSettings, string name, string type) 
            : base(connectionSettings, name)
        {
            this.Type = type;
            try
            {
                this.Channel.ExchangeDeclare(name, type);
            }
            catch (IOException)
            {
                throw new Exception("Exchange Declare is failed.");
            }
        }
    }
}