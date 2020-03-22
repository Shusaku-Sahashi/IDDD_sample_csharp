using System.Collections.Generic;
using System.Linq;
using Dapper;
using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Domain.Model.Tenant;
using IDDDAgilePm.Port.Adapter.Persistence.DataModel;
using IDDDCommon.Port.Adapter.Persistence.Mysql;

namespace IDDDAgilePm.Port.Adapter.Persistence
{
    internal class MysqlTeamMemberRepository : ITeamMemberRepository
    {
        private MySqlProvider _provider = new MySqlProvider();

        public IEnumerable<TeamMember> AllTeamMembers(TenantId tenantId)
        {
            using var conn = _provider.GetConnection();
            var sql = $@"
select
    tenant_id as {nameof(TeamMemberDTO.TenantId)},
    username as {nameof(TeamMemberDTO.Username)},
    first_name as {nameof(TeamMemberDTO.FirstName)},
    last_name as {nameof(TeamMemberDTO.LastName)},
    email_address as {nameof(TeamMemberDTO.EmailAddress)},
    enabled as {nameof(TeamMemberDTO.Enabled)},
    email_address_update_timestamp as {nameof(TeamMemberDTO.EmailAddressUpdateTimestamp)},
    enabling_toggle_timestamp as {nameof(TeamMemberDTO.EnablingToggleTimestamp)},
    name_update_timestamp as {nameof(TeamMemberDTO.NameUpdateTimestamp)}
from
     tbl_team_member
where
    tenant_id = @TenantId;
    ";
            var teamMemberDTOs = conn.Query<TeamMemberDTO>(sql, new {TenantId = tenantId.Id});

            return teamMemberDTOs.
                Select(c => new TeamMember(
                                new TenantId(c.TenantId),
                                c.Username,
                                c.LastName,
                                c.FirstName,
                                c.EmailAddress,
                                new MemberChangeTracker(c.EmailAddressUpdateTimestamp, c.EnablingToggleTimestamp, c.NameUpdateTimestamp)));
        }

        public TeamMember TeamMemberOfIdentified(TeamMemberId teamMemberId)
        {
            
            using var conn = _provider.GetConnection();
            var sql = $@"
select
    tenant_id as {nameof(TeamMemberDTO.TenantId)},
    username as {nameof(TeamMemberDTO.Username)},
    first_name as {nameof(TeamMemberDTO.FirstName)},
    last_name as {nameof(TeamMemberDTO.LastName)},
    email_address as {nameof(TeamMemberDTO.EmailAddress)},
    enabled as {nameof(TeamMemberDTO.Enabled)},
    email_address_update_timestamp as {nameof(TeamMemberDTO.EmailAddressUpdateTimestamp)},
    enabling_toggle_timestamp as {nameof(TeamMemberDTO.EnablingToggleTimestamp)},
    name_update_timestamp as {nameof(TeamMemberDTO.NameUpdateTimestamp)}
from
     tbl_team_member
where
    tenant_id = @TenantId
    and username = @Username;
    ";
            var teamMemberDTO = conn.QueryFirst<TeamMemberDTO>(sql, new {TenantId = teamMemberId.TenantId.Id, teamMemberId.Id});

            return new TeamMember( new TenantId(teamMemberDTO.TenantId),
                                    teamMemberDTO.Username,
                                    teamMemberDTO.LastName,
                                    teamMemberDTO.FirstName,
                                    teamMemberDTO.EmailAddress,
                                    new MemberChangeTracker(teamMemberDTO.EmailAddressUpdateTimestamp, teamMemberDTO.EnablingToggleTimestamp, teamMemberDTO.NameUpdateTimestamp));
        }

        public void Save(TeamMember teamMember)
        {
            using var conn = _provider.GetConnection();
            var sql = $@"
insert into tbl_team_member
    (tenant_id, 
    username, 
    first_name, 
    last_name, 
    email_address, 
    enabled, 
    email_address_update_timestamp, 
    enabling_toggle_timestamp, 
    name_update_timestamp)
values (
    @TenantId, 
    @Username, 
    @FirstName, 
    @LastName, 
    @EmailAddress, 
    @Enabled, 
    @EmailAddressUpdateTimestamp,
    @EnablingToggleTimestamp, 
    @NameUpdateTimestamp);
            ";

            conn.Execute(sql,
                new
                {
                    TenantId = teamMember.TenantId.Id,
                    Username = teamMember.Username,
                    FirstName = teamMember.FirstName,
                    LastName = teamMember.LastName,
                    EmailAddress = teamMember.EmailAddress,
                    Enabled = teamMember.Enabled,
                    EmailAddressUpdateTimestamp = teamMember.ChangeTracker.EmailAddressChangedOn,
                    EnablingToggleTimestamp = teamMember.ChangeTracker.EnablingOn,
                    NameUpdateTimestamp = teamMember.ChangeTracker.NameChangedOn
                });
        }

        public void Save(IEnumerable<TeamMember> teamMembers)
        {
            foreach (var member in teamMembers)
            {
                this.Save(member);
            }
        }

        public void Remove(TeamMember teamMember)
        {
            using var conn = _provider.GetConnection();
            var sql = $@"
delete from tbl_team_member
where
    tenant_id = @TenantId
    and username = @Username;
            ";

            conn.Execute(sql, new {TenantId = teamMember.TeamMemberId().TenantId.Id, Username = teamMember.TeamMemberId().Id});
        }

        public void RemoveAll(IEnumerable<TeamMember> teamMembers)
        {
            foreach (var member in teamMembers)
            {
                this.Remove(member);
            }
        }
    }
}