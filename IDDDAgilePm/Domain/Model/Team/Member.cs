using System;
using IDDDAgilePm.Domain.Model.Tenant;

namespace IDDDAgilePm.Domain.Model.Team
{
    internal abstract class Member
    {
        private TenantId _tenantId;
        private string _lastName;
        private string _firstName;
        private string _username;
        private string _emailAddress;
        private bool _enabled = true;
        private MemberChangeTracker _changeTracker;

        public Member(TenantId tenantId, string username, string lastName, string firstName, string emailAddress,
            DateTime initializedOn)
        {
            this.TenantId = tenantId;
            this.Username = username;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.EmailAddress = emailAddress;

            this.ChangeTracker = new MemberChangeTracker(initializedOn, initializedOn, initializedOn);
        }
        
        public Member(TenantId tenantId, string username, string lastName, string firstName, string emailAddress, MemberChangeTracker changeTracker)
        {
            this.TenantId = tenantId;
            this.Username = username;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.EmailAddress = emailAddress;
            this.ChangeTracker = changeTracker;
        }

        public void ChangeEmailAddress(string emailAddress, DateTime on)
        {
            if (!this.ChangeTracker.CanEmailAddressChange(on) || this.EmailAddress.Equals(emailAddress)) return;
            this.EmailAddress = emailAddress;
            this.ChangeTracker = this.ChangeTracker.EmailAddressChanged(on);
        }
        
        public void ChangeName(string lastName, string firstName, DateTime on)
        {
            if (!this.ChangeTracker.CanNameChange(on)) return;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ChangeTracker = this.ChangeTracker.NameChanged(on);
        }

        public void Enable(DateTime on)
        {
            if (!this.ChangeTracker.CanToggleEnabling(on) || this.Enabled == true) return;
            this.Enabled = true;
            this.ChangeTracker = this.ChangeTracker.Enabled(on);
        }

        public void Disable(DateTime on)
        {
            if (!this.ChangeTracker.CanToggleEnabling(on)) return;
            this.Enabled = false;
            this.ChangeTracker = this.ChangeTracker.Enabled(on);
        }

        public TenantId TenantId
        {
            get => _tenantId;
            private set => _tenantId = value;
        }

        public string LastName
        {
            get => _lastName;
            private set => _lastName = value;
        }

        public string FirstName
        {
            get => _firstName;
            private set => _firstName = value;
        }

        public string Username
        {
            get => _username;
            private set => _username = value;
        }

        public string EmailAddress
        {
            get => _emailAddress;
            private set => _emailAddress = value;
        }

        public bool Enabled
        {
            get => _enabled;
            private set => _enabled = value;
        }

        public MemberChangeTracker ChangeTracker
        {
            get => _changeTracker;
            private set => _changeTracker = value;
        }
    }
}