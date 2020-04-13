using System;
using System.Collections.Generic;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Domain.Model.Team
{
    internal class ProductOwnerId : ValueObject
    {
        private TenantId _tenantId;
        private string _id;
        
        public ProductOwnerId(TenantId tenantId, string id) : base()
        {
            this.TenantId = tenantId;
            this.Id = id;
        }

        public override bool Equals(object obj)
        {
            var equalObject = false;
            
            if (obj != null && this.GetType() == obj.GetType())
            {
                var typedObject = (ProductOwnerId) obj;
                equalObject =
                    typedObject.TenantId.Equals(this.TenantId) &&
                    typedObject.Id == this.Id;
            }

            return equalObject;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.TenantId.GetHashCode(), this.Id.GetHashCode());
        }

        public TenantId TenantId
        {
            get => _tenantId;
            private set => _tenantId = value;
        }

        public string Id
        {
            get => _id;
            private set => _id = value;
        }
    }
}