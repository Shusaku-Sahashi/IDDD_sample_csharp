using System.Collections.Generic;
using Dapper;
using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Domain.Model.Tenant;
using IDDDCommon.Port.Adapter.Persistence.Mysql;

namespace IDDDAgilePm.Port.Adapter.Persistence
{
    internal class MysqlProductOwnerRepository : IProductOwnerRepository
    {
        private MySqlProvider _provider;
        
        public IEnumerable<ProductOwner> AllProductOwners(TenantId tenantId)
        {
            throw new System.NotImplementedException();
        }

        public ProductOwner ProductOwnerOfIdentified(ProductOwnerId productOwnerId)
        {
            throw new System.NotImplementedException();
        }

        public void Save(ProductOwner productOwner)
        {
            throw new System.NotImplementedException();
        }

        public void Save(IEnumerable<ProductOwner> productOwners)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(ProductOwner productOwner)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAll(IEnumerable<ProductOwner> productOwners)
        {
            throw new System.NotImplementedException();
        }
    }
}