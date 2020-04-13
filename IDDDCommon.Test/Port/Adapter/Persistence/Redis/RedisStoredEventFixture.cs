using System;
using System.Linq;
using IDDDCommon.Domain.Model.Process;
using IDDDCommon.Event.Source;
using IDDDCommon.Port.Adapter.Persistence.Redis;
using IDDDCommon.Test.Port.Adapter.Messaging.Rabbitmq;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using ServiceStack;

namespace IDDDCommon.Test.Port.Adapter.Persistence.Redis
{
    [TestFixture]
    public class RedisStoredEventFixture
    {
        private RedisEventStore _redisEventStore = new RedisEventStore();

        [TearDown]
        public void StartUp()
        {
            _redisEventStore.Purge();
        }

        [Test]
        public void CanGetAllStoredEventAll()
        {
            Enumerable
                .Range(1, 10).Select(n => new MyDomainEvent($"test {n}")).ToList()
                .ForEach(e => _redisEventStore.Store(e));

            var actual = _redisEventStore.AllStoredEventSince(1);

            Assert.That(actual.FirstOrDefault()?.EventBody, 
                Is.EqualTo(EventSerializer.Serialize(new MyDomainEvent("test 1"))));
        }
    }
}