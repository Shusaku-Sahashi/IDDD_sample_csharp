using System.Transactions;
using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Application.Team
{
    internal class TeamApplicationService
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IProductOwnerRepository _productOwnerRepository;
        private readonly ApplicationServiceLifeCycle _applicationServiceLifeCycle;

        public TeamApplicationService(
            ITeamMemberRepository teamMemberRepository,
            IProductOwnerRepository productOwnerRepository,
            ApplicationServiceLifeCycle applicationServiceLifeCycle)
        {
            this._productOwnerRepository = productOwnerRepository;
            this._applicationServiceLifeCycle = applicationServiceLifeCycle;
            this._teamMemberRepository = teamMemberRepository;
        }

        public void EnableProductOwner(EnableProductOwnerCommand command)
        {
            using (_applicationServiceLifeCycle.Begin())
            {
                var tenantId = new TenantId(command.TenantId);

                var id = new ProductOwnerId(tenantId, command.Username);

                using var tx = new TransactionScope(TransactionScopeOption.Required);
                var productOwner = _productOwnerRepository.ProductOwnerOfIdentified(id);

                if (productOwner != null)
                {
                    productOwner.Enable(command.OccuredOn);
                }
                else
                {
                    productOwner = new ProductOwner(tenantId, command.Username, command.LastName, command.FirstName,
                        command.EmailAddress, command.OccuredOn);
                }
                        
                _productOwnerRepository.Save(productOwner);
                    
                tx.Complete();
            }
        }
    }
}