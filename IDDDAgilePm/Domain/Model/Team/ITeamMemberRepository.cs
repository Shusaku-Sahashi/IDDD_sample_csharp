using System.Collections.Generic;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Domain.Model.Team
{
    internal interface ITeamMemberRepository
    {
        IEnumerable<TeamMember> AllTeamMembers(TenantId tenantId);
        TeamMember TeamMemberOfIdentified(TeamMemberId teamMemberId);
        void Save(TeamMember teamMember);
        void Save(IEnumerable<TeamMember> teamMembers);
        void Remove(TeamMember teamMember);
        void RemoveAll(IEnumerable<TeamMember> teamMembers);
    }
}