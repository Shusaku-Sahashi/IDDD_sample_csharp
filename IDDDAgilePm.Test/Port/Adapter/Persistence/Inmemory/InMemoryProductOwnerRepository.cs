using System.Collections.Generic;
using System.Linq;
using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Test.Port.Adapter.Persistence.Inmemory
{
    internal class InMemoryProductOwnerRepository : IProductOwnerRepository
    {
        private readonly Dictionary<ProductOwnerId, ProductOwner> _data = new Dictionary<ProductOwnerId, ProductOwner>();
        
        public IEnumerable<ProductOwner> AllProductOwners(TenantId tenantId)
        {
            return this._data
                .Where(kv => kv.Value.TenantId.Equals(tenantId))
                .Select(kv => kv.Value);
        }

        public ProductOwner ProductOwnerOfIdentified(ProductOwnerId productOwnerId)
        {
            return this._data
                .Where(kv => kv.Key.Equals(productOwnerId))
                .Select(kv => kv.Value)
                .FirstOrDefault();
        }

        public void Save(ProductOwner productOwner)
        {
            this._data.Add(productOwner.ProductOwnerId(), productOwner);
        }

        public void Save(IEnumerable<ProductOwner> productOwners)
        {
            productOwners.ToList().ForEach(this.Save);
        }

        public void Remove(ProductOwner productOwner)
        {
            this._data.Remove(productOwner.ProductOwnerId());
        }

        public void RemoveAll(IEnumerable<ProductOwner> productOwners)
        {
            productOwners.ToList().ForEach(this.Remove);
        }
    }
}