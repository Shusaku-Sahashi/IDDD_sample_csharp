using IDDDCommon.Domain.Model.Process;
using Newtonsoft.Json;

namespace IDDDCommon.Event.Source
{
    public class EventSerializer
    {
        internal static string Serialize(DomainEvent domainEvent)
        {
            return JsonConvert.SerializeObject(domainEvent);
        }
    }
}