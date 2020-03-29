using System;
using System.Runtime.CompilerServices;
using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class Exchange : BrokerChannel
    {
        public Exchange(string name) : base()
        {
            this.Name = name;
            this.ExchangeName = name;
            this.Channel.ExchangeDeclare(this.ExchangeName, ExchangeType.Fanout);
        }
    }
}