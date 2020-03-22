using System;

namespace IDDDAgilePm.Domain.Model.Tenant
{
    internal class TenantId : ValueObject
    {
        private string _id;
        
        public string Id
        {
            get => _id;
            private set
            {
                this.AssertArgumentNotEmpty(value, "tenant identity is required.");
                this.AssertArgumentLength(value, 36, "tenant identity is required less than 36 characters.");

                this._id = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) return false;

            var typedObject = (TenantId) obj;

            return this.Id == typedObject.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id.GetHashCode());
        }

        public TenantId(string id) : this() { this.Id = id; }
        
        private TenantId() { }
    }
}