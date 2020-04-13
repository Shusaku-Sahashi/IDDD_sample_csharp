using System;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Application.Team
{
    public class EnableMemberCommand
    {
        public string TenantId { get; private set; }
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public DateTime OccuredOn { get; private set; }

        public EnableMemberCommand(string tenantId, string username, string firstName, string lastName,
            string emailAddress, DateTime occuredOn)
        {
            TenantId = tenantId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            OccuredOn = occuredOn;
        }
    }
}