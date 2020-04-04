using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class Queue : BrokerChannel 
    {
        public Queue(string name, BrokerChannel exchange) : base()
        {
            this.Name = name;
            this.ExchangeName = exchange.Name;
            this.Channel.QueueDeclare(this.Name, false, false, false, null);
            this.Channel.QueueBind(this.Name, this.ExchangeName, "");
        }
    }
}