using System;
using System.Collections;
using System.Collections.Generic;
using IDDDCommon.Domain.Model.Process;

namespace IDDDCommon.Event.Source
{
    public interface EventStore
    {
        IEnumerable<StoredEvent> AllStoredEventSince(long from);
        void Store(DomainEvent domainEvent);
        void Purge(); // Only User Test
    }
}