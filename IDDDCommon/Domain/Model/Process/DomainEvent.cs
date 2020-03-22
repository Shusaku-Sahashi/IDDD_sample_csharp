using System;

namespace IDDDCommon.Domain.Model.Process
{
    public interface DomainEvent
    {
        int EventVersion { get; } 
        string EventId { get; }
        DateTime OccurredOn { get; }
    }
}