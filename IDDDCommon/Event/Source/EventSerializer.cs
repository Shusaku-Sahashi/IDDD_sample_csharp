using System.Text.Json;
using IDDDCommon.Domain.Model.Process;

namespace IDDDCommon.Event.Source
{
    public class EventSerializer
    {
        internal static string Serialize(DomainEvent domainEvent)
        {
            return JsonSerializer.Serialize(domainEvent);
        }
    }
}