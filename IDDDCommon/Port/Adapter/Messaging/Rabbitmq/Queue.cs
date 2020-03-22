using RabbitMQ.Client;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class Queue : BrokerChannel
    {
        public static Queue Instance(Exchanger exchanger, string name)
        {
            var queue = new Queue(exchanger, name);
            queue.Channel.QueueBind(queue.Name ,exchanger.Name, "");

            return queue;
        }
        
        public Queue(BrokerChannel channel, string name) : base(channel)
        {
            var result = this.Channel.QueueDeclare(name, false, false, false, null);
            this.Name = result.QueueName;
        }
    }
}