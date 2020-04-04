using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public class MessageConsumer
    {
        private Queue Queue { get; set; }
        private bool AutoAcknowledged { get; set; }
        private static bool _isClosed;

        public MessageConsumer(Queue queue)
        {
            this.Queue = queue;
        }

        public void ReceiveFor(IMessageListener messageListener)
        {
            var consumer = DispatchingConsumer.Instance(this.Queue.Channel, messageListener, _isClosed);

            this.Queue.Channel.BasicConsume(
                this.Queue.Name,
                AutoAcknowledged,
                consumer);
        }

        private static void Close()
        {
            // Interlocked.Exchange<>(ref _isClosed, true);
        }

        private class DispatchingConsumer : EventingBasicConsumer
        {
            public static DispatchingConsumer Instance(IModel channel, IMessageListener messageListener,
                in bool isClosed)
            {
                var consumer = new DispatchingConsumer(channel, messageListener, isClosed);

                consumer.Received += (ch, ea) =>
                {
                    if (_isClosed)
                    {
                        Close(); 
                        channel.Close();
                        return;
                    }

                    Handle(channel, messageListener, ea);
                };

                consumer.Shutdown += (ch, sa) =>
                {
                    Close();
                    channel.Close();
                };

                return consumer;
            }

            private static void Handle(IModel channel, IMessageListener messageListener, BasicDeliverEventArgs ea)
            {
                try
                {
                    messageListener.HandleMessage(
                        ea.BasicProperties.Type,
                        ea.BasicProperties.MessageId,
                        new DateTime(ea.BasicProperties.Timestamp.UnixTime),
                        Encoding.UTF8.GetString(ea.Body),
                        ea.DeliveryTag,
                        ea.Redelivered);

                    Ack(channel, ea);
                }
                catch (Exception)
                {
                    // TODO: Loggerに後に変更する。
                    // TODO: 再送するように変更する。
                    Console.WriteLine("エラーが発生したため終了します。");
                    Nac(channel, ea, false);
                }
            }

            private static void Ack(IModel channel, BasicDeliverEventArgs ea)
            {
                channel.BasicAck(ea.DeliveryTag, false);
            }

            private static void Nac(IModel channel, BasicDeliverEventArgs ea, bool isRetry)
            {
                channel.BasicNack(ea.DeliveryTag, false, isRetry);
            }

            private DispatchingConsumer(IModel channel, IMessageListener messageListener, in bool isClosed) :
                base(channel)
            {
            }
        }
    }
}