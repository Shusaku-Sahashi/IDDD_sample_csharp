using System;
using System.Runtime.CompilerServices;
using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class Exchange : BrokerChannel
    {
        public Exchange(Exchanges exchanges) : base()
        {
            this.Name = exchanges.ToString();
            this.ExchangeName = exchanges.ToString();
            this.Channel.ExchangeDeclare(this.ExchangeName, ExchangeType.Fanout);
        }
    }
}