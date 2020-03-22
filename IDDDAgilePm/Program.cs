using System;
using IDDDCommon.Port.Adapter.Messaging.Rabbitmq;

namespace IDDDAgilePm
{
    class Program
    {
        static void Main(string[] args)
        {
            new RabbitMQMessageProducer()._Send("fanout-test");
        }
    }
}