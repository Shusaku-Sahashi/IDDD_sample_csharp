using System;
using System.Text.Json;
using IDDDCommon.Notification;
using NUnit.Framework;

namespace IDDDCommon.Test.Media
{
    [TestFixture]
    public class AbstractJsonMediaReaderFixture
    {
        private const string Message = 
            "{\"NotificationId\":1,\"DomainEvent\":{\"EventVersion\":1,\"EventId\":\"88b0543c-e17b-4edb-a7c5-4ce8f0d8bb36\",\"OccurredOn\":\"2020-03-28T19:04:53.191488+09:00\",\"Message\":\"serialize-test\"},\"TypeName\":\"IDDDCommon.Test.NotificationSerializerFixture+MyDomainEvent, IDDDCommon.Test\",\"OccurredOn\":\"2020-03-28T19:04:53.191488+09:00\",\"Version\":1}";
            
        [Test]
        public void CanDeserializeInt()
        {
            using var reader = new NotificationReader(Message);
            var actual = reader.StringEventValue("EventVersion");
            
            Assert.That(actual, Is.EqualTo("1"));
        }
        
        [Test]
        public void CanDeserializeString()
        {
            using var reader = new NotificationReader(Message);
            var actual = reader.NotificationId();
            
            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void CanDeserializeDateTime()
        {
            using var reader = new NotificationReader(Message);
            var actual = reader.OccurredOn();
            
            Assert.That(actual, Is.EqualTo(DateTime.Parse("2020-03-28T19:04:53.191488+09:00")));
        }

        [Test]
        public void CanSelectNestedValue()
        {
            using var reader = new NotificationReader(Message);
            var actual = reader.StringEventValue("EventVersion");
            
            Assert.That(actual, Is.EqualTo("1"));
        }
        
        [Test]
        public void CanRaiseExceptionWhenCanNotFindKeyword()
        {
            using var reader = new NotificationReader(Message);

            Assert.Catch<JsonException>(() => reader.StringEventValue("Dummy"));
        }
    }
}