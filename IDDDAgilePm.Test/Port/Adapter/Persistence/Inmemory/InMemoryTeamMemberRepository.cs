using System.Collections.Generic;
using System.Linq;
using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Test.Port.Adapter.Persistence.Inmemory
{
    internal class InMemoryTeamMemberRepository : ITeamMemberRepository
    {
        private readonly Dictionary<TeamMemberId, TeamMember> _data = new Dictionary<TeamMemberId, TeamMember>();
        
        public IEnumerable<TeamMember> AllTeamMembers(TenantId tenantId)
        {
            return this._data
                .Where(kv => kv.Value.TenantId.Equals(tenantId))
                .Select(kv => kv.Value);
        }

        public TeamMember TeamMemberOfIdentified(TeamMemberId teamMemberId)
        {
            return this._data
                .Where(kv => kv.Key.Equals(teamMemberId))
                .Select(kv => kv.Value)
                .FirstOrDefault();
        }

        public void Save(TeamMember teamMember)
        {
            this._data.Add(teamMember.TeamMemberId(), teamMember);
        }

        public void Save(IEnumerable<TeamMember> teamMembers)
        {
            teamMembers.ToList().ForEach(this.Save);
        }

        public void Remove(TeamMember teamMember)
        {
            this._data.Remove(teamMember.TeamMemberId());
        }

        public void RemoveAll(IEnumerable<TeamMember> teamMembers)
        {
            teamMembers.ToList().ForEach(this.Remove);
        }
    }
}