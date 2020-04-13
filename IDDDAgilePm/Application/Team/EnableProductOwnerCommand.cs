using System;

namespace IDDDAgilePm.Application.Team
{
    public class EnableProductOwnerCommand : EnableMemberCommand
    {
        public EnableProductOwnerCommand(string tenantId, string username, string firstName, string lastName,
            string emailAddress, DateTime occuredOn) : base(tenantId, username, firstName, lastName, emailAddress,
            occuredOn) { }
    }
}