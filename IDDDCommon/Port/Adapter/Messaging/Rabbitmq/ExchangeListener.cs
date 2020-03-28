namespace IDDDCommon.Port.Adapter.Messaging.Rabbitmq
{
    public abstract class ExchangeListener
    {
        public ExchangeListener()
        {
        }

        public abstract void ExchangeName();
    }
}