using System;

namespace IDDDAgilePm.Application.Team
{
    public class EnableTeamMemberCommand : EnableMemberCommand
    {
        public EnableTeamMemberCommand(
            string tenantId, 
            string username, 
            string firstName, 
            string lastName,
            string emailAddress, 
            DateTime occuredOn) : base(tenantId, username, firstName, lastName, emailAddress, occuredOn) { }
    }
}