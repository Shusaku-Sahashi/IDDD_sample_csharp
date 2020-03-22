using System.Collections.Generic;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Domain.Model.Team
{
    internal interface TeamRepository
    {
        IEnumerable<Team> AllTeams(TenantId tenantId);
        Team TeamNamed(TenantId tenantId, string teamName);
        void Save(Team team);
        void Save(IEnumerable<Team> teams);
        void Remove(Team team);
        void RemoveAll(IEnumerable<Team> teams);
    }
}