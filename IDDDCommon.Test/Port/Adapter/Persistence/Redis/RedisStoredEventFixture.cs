using System;
using System.Linq;
using IDDDCommon.Event.Source;
using IDDDCommon.Port.Adapter.Persistence.Redis;
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
                .Range(1, 10).Select(n => new StoredEvent(n, $"test {n}", "test-type", DateTime.Now)).ToList()
                .ForEach(e => _redisEventStore.Store(e));

            var actual = _redisEventStore.AllStoredEventSince(1);
            
            Assert.That(actual.FirstOrDefault()?.EventBody,  Is.EqualTo("test 1"));
        }
    }
}