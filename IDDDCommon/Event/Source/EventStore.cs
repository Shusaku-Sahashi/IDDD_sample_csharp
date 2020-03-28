using System;
using System.Collections;
using System.Collections.Generic;

namespace IDDDCommon.Event.Source
{
    public interface EventStore
    {
        IEnumerable<StoredEvent> AllStoredEventSince(long from);
        void Store(StoredEvent storedEvent);
        void Purge(); // Only User Test
    }
}