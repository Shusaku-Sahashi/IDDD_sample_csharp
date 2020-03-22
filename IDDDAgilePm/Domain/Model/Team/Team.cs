using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Domain.Model.Team
{
    internal class Team : Entity
    {
        private ProductOwner _productOwner;
        private HashSet<TeamMember> _teamMembers;
        private TenantId _tenantId;
        private string _teamName;

        /// <summary>
        /// ProductOwnerを指定して生成する場合
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="teamName"></param>
        /// <param name="productOwner"></param>
        public Team(TenantId tenantId, string teamName, ProductOwner productOwner) : this(tenantId, teamName)
        {
            ProductOwner = productOwner;
        }
        
        public Team(TenantId tenantId, string teamName) : this()
        {
            TenantId = tenantId;
            TeamName = teamName;
        }

        public IReadOnlyCollection<TeamMember> AllTeamMembers() => new ReadOnlyCollection<TeamMember>(this.TeamMembers.ToList());

        public void AssignProductOwner(ProductOwner productOwner)
        {
            AssertArgumentEquals(this.TenantId, productOwner.TenantId, "Product owner must be of the same tenant.");
            this.ProductOwner = productOwner;
        }

        public void AssignTeamMember(TeamMember teamMember)
        {
            AssertArgumentEquals(this.TenantId, teamMember.TenantId, "Team member must be of same tenant.");
            this.TeamMembers.Add(teamMember);
        }

        public bool IsTeamMember(TeamMember teamMember)
        {
            AssertArgumentEquals(this.TenantId, teamMember.TenantId, "Team member must be of same tenant.");
            return this.TeamMembers.Contains(teamMember);
        }

        public void RemoveTeamMember(TeamMember teamMember)
        {
            AssertArgumentEquals(this.TenantId, teamMember.TenantId, "Team member must be of same tenant.");
            TeamMembers.Remove(teamMember);
        }
        
        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType()) return false;

            var typedObject = (Team) obj;
            return typedObject?.TenantId == this.TenantId &&
                   typedObject?.TeamName == this.TeamName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.TenantId.GetHashCode(), this.TeamName.GetHashCode());
        }

        public ProductOwner ProductOwner
        {
            get => _productOwner;
            private set
            {
                AssertArrangeNotNull(value, "The ProductOwner must be provide.");
                AssertArgumentEquals(this.TenantId, value.TenantId, "The ProductOwner must be same tenant.");
                _productOwner = value;  
            } 
        }

        public TenantId TenantId
        {
            get => _tenantId;
            private set
            {
                AssertArrangeNotNull(value, "The TenantId must be provide.");
                _tenantId = value;   
            }
        }

        public string TeamName
        {
            get => _teamName;
            private set
            {
                AssertArrangeNotNull(value, "The TenantName must be provide.");
                AssertArgumentLength(value, 100, "The name must be 100 character or less.");
                _teamName = value;
            }
        }

        private HashSet<TeamMember> TeamMembers
        {
            get => _teamMembers;
            set => _teamMembers = value;
        }

        private Team() : base() => this.TeamMembers = new HashSet<TeamMember>();
    }
}