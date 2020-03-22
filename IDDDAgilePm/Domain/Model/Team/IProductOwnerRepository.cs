using System.Collections.Generic;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Domain.Model.Team
{
    internal interface IProductOwnerRepository
    {
        IEnumerable<ProductOwner> AllProductOwners(TenantId tenantId);
        ProductOwner ProductOwnerOfIdentified(ProductOwnerId productOwnerId);
        void Save(ProductOwner productOwner);
        void Save(IEnumerable<ProductOwner> productOwners);
        void Remove(ProductOwner productOwner);
        void RemoveAll(IEnumerable<ProductOwner> productOwners);
    }
}