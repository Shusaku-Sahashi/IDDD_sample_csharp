using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Domain.Model.Team
{
    internal class TeamMemberId : ValueObject
    {
        private TenantId _tenantId;
        private string _id;
        
        public TeamMemberId(TenantId tenantId, string id) : base()
        {
            this.TenantId = tenantId;
            this.Id = id;
        }
        
        public TenantId TenantId
        {
            get => _tenantId;
            private set
            {
                AssertArrangeNotNull(value, "The tenantId must be provided");
                _tenantId = value;  
            } 
        }

        public string Id
        {
            get => _id;
            private set
            {
                AssertArgumentNotEmpty(value, "The id must not be provided.");
                AssertArgumentLength(value, 36, "The id must be 36 characters or less.");
                _id = value;   
            }
        }
    }
}