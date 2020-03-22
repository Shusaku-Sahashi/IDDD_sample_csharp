using System;
using System.Collections.Generic;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Domain.Model.Team
{
    internal class TeamMember : Member
    {
        public TeamMember(
            TenantId tenantId,
            string username,
            string lastName,
            string firstName,
            string emailAddress,
            DateTime initializedOn) : base(tenantId, username, lastName, firstName, emailAddress, initializedOn) { }
        
        public TeamMember(
            TenantId tenantId,
            string username,
            string lastName,
            string firstName,
            string emailAddress,
            MemberChangeTracker changeTracker) : base(tenantId, username, lastName, firstName, emailAddress, changeTracker) { }
        
        public TeamMemberId TeamMemberId() => new TeamMemberId(this.TenantId, this.Username);

        public override bool Equals(object obj)
        {
            var equalObjects = false;

            if (obj != null && this.GetType() == obj.GetType())
            {
                var teamMember = (TeamMember) obj;
                equalObjects =
                    this.TenantId == teamMember.TenantId &&
                    this.Username == teamMember.Username;
            }

            return equalObjects;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.TenantId.GetHashCode(), this.Username.GetHashCode());
        }
    }
}