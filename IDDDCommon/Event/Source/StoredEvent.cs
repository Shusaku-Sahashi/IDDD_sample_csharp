using System;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text.Json;
using IDDDCommon.Domain.Model.Process;
using Newtonsoft.Json;

namespace IDDDCommon.Event.Source
{
    public class StoredEvent
    {
        public long Id { get; set; }
        public string EventBody { get; set; }
        public string TypeName { get; set; }
        public DateTime OccuredOn { get; set; }

        public StoredEvent(long id, string eventBody, string typeName, DateTime occuredOn)
        {
            this.Id = id;
            this.EventBody = eventBody;
            this.TypeName = typeName;
            this.OccuredOn = occuredOn;
        }

        public T ToDomainEvent<T>() where T : class, DomainEvent // as T でキャストを行うので、
        {
            var type = Type.GetType(this.TypeName);
            T typedObject;
            
            if ((typedObject = JsonConvert.DeserializeObject(this.EventBody, type) as T) == null)
                throw new Exception($"Can not cast to {type}. eventBody: {this.EventBody}");
            
            return typedObject;
        }

        public override bool Equals(object obj)
        {
            
            if (obj == null || obj.GetType() != this.GetType()) return false;

            var typedObject = (StoredEvent) obj;

            return this.Id == typedObject.Id &&
                   this.EventBody == typedObject.EventBody &&
                   this.TypeName == typedObject.TypeName &&
                   this.OccuredOn == typedObject.OccuredOn;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.EventBody.GetHashCode(), this.TypeName.GetHashCode(), this.OccuredOn);
        }
    }
}