using System;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Port.Adapter.Persistence.DataModel
{
    internal class TeamMemberDTO
    {
        public string TenantId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool Enabled { get; set; }
        public DateTime EmailAddressUpdateTimestamp { get; set; }
        public DateTime EnablingToggleTimestamp { get; set; }
        public DateTime NameUpdateTimestamp { get; set; }
    }
}