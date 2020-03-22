using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace IDDDCommon.Domain.Model.Process
{
    public class DomainEventPublisher
    {
        /// <summary>
        /// NOTE: DomainPublisherはThreadごとにDomainEventのリストを保持し、リクエストが新たにあった場合は
        /// </summary>
        private static ThreadLocal<DomainEventPublisher> _publisher = new ThreadLocal<DomainEventPublisher>(() => new DomainEventPublisher());
        
        private readonly List<Delegate> _actions = new List<Delegate>();

        public static DomainEventPublisher Current()
        {
            return _publisher.Value;
        }

        public IDisposable Register<T>(Action<T> action) where T : DomainEvent
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            _actions.Add(action);

            return new DomainEventRegistrationRemover(() => _actions.Clear());
        }

        public void Publish<T>(T eventArg) where T : DomainEvent
        {
            foreach (var typedAction in _actions.Select(action => action as Action<T>))
            {
                typedAction?.Invoke(eventArg);
            }
        }

        private DomainEventPublisher() { }
        
        private sealed class DomainEventRegistrationRemover : IDisposable
        {
            private readonly Action _callOnDispose;

            public DomainEventRegistrationRemover(Action toCall)
            {
                _callOnDispose = toCall;
            }

            public void Dispose()
            {
                _callOnDispose();
            }
        }
    }
}