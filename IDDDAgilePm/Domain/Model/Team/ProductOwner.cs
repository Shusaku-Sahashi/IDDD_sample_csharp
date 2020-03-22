using System;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Domain.Model.Team
{
    internal class ProductOwner : Member
    {
        public ProductOwner(
            TenantId tenantId,
            string username,
            string lastName, 
            string firstName, 
            string emailAddress,
            DateTime initializedOn) : base(tenantId, username, lastName, firstName, emailAddress, initializedOn) { }
        
        public ProductOwnerId ProductOwnerId() => new ProductOwnerId(this.TenantId, this.Username);

        public override bool Equals(object obj)
        {
            var objectEqual = false;
            if (obj == null || obj.GetType() != this.GetType()) return objectEqual;
            
            var typedObject = (ProductOwner) obj;
            objectEqual =
                typedObject.TenantId == this.TenantId &&
                typedObject.Username == this.Username;

            return objectEqual;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.TenantId, this.Username);
        }
    }
}