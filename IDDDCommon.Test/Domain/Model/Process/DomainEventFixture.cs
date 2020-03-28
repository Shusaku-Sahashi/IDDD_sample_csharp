using System;
using IDDDCommon.Domain.Model.Process;
using NUnit.Framework;

namespace IDDDCommon.Test.Domain.Model.Process
{
    [TestFixture]
    public class DomainEventFixture
    {
        [Test]
        public void CanPublicDomainEvent()
        {
            var result = "";
            var expected = "test-message";
            
            using (DomainEventPublisher.Current()
                .Register<MyDomainEvent>(eventArgs => result = eventArgs.Message))
            {
                DomainEventPublisher.Current()
                    .Publish(new MyDomainEvent(expected));
            }
            
            Assert.That(result, Is.EqualTo(expected));
        }
        
        private class MyDomainEvent : DomainEvent
        {
            private const int _domainEventVersion = 1;
            public MyDomainEvent(string message)
            {
                this.EventVersion = _domainEventVersion;
                this.EventId = Guid.NewGuid().ToString();
                this.OccurredOn = DateTime.Now;
                this.Message = message;
            }

            public int EventVersion { get; }
            public string EventId { get; private set; }
            public DateTime OccurredOn { get; private set; }
            public string Message { get; private set; }
        }
    }
}