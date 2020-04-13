using IDDDCommon.Event.Source;
using IDDDCommon.Notification;
using IDDDCommon.Port.Adapter.Messaging.Rabbitmq;
using IDDDCommon.Port.Adapter.Persistence.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IDDDCommon.Config
{
    public class CommonServiceProvider
    {
        public static void RegisterServices(ServiceCollection services)
        {
            services.AddSingleton<EventStore, RedisEventStore>();
            services.AddSingleton<IPublishedNotificationTrackerStore, RedisPublishNotificationTrackerStore>();
            services.AddSingleton<MessageProducer>();
            services.AddSingleton<NotificationPublisher>();
            services.AddSingleton( p => new MessageProducer(p.GetRequiredService<Exchange>()) );
        }

        private CommonServiceProvider() { }
    }
}