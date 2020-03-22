using System;

namespace IDDDAgilePm.Domain.Model.Team
{
    internal class MemberChangeTracker : ValueObject
    {
        private DateTime _emailAddressChangedOn;
        private DateTime _enablingOn;
        private DateTime _nameChangedOn;

        public MemberChangeTracker(DateTime emailAddressChangedOn, DateTime enablingOn, DateTime nameChangedOn)
        {
            this.EmailAddressChangedOn = emailAddressChangedOn;
            this.EnablingOn = enablingOn;
            this.NameChangedOn = nameChangedOn;
        }

        public bool CanEmailAddressChange(DateTime on) => this.EmailAddressChangedOn < on;
        public bool CanToggleEnabling(DateTime on) => this.EnablingOn < on;
        public bool CanNameChange(DateTime on) => this.NameChangedOn < on;

        public MemberChangeTracker EmailAddressChanged(DateTime on)
        {
            return new MemberChangeTracker(on, this.EnablingOn, this.NameChangedOn);
        }

        public MemberChangeTracker Enabled(DateTime on)
        {
            return new MemberChangeTracker(this.EmailAddressChangedOn, on, this._emailAddressChangedOn);
        }

        public MemberChangeTracker NameChanged(DateTime on)
        {
            return new MemberChangeTracker(this.EmailAddressChangedOn, this._enablingOn, on);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) return false;
            var typedObject = (MemberChangeTracker) obj;
            
            return typedObject.EnablingOn == this.EnablingOn &&
                   typedObject.NameChangedOn == this.NameChangedOn &&
                   typedObject.EmailAddressChangedOn == this.EmailAddressChangedOn;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.EnablingOn, this.NameChangedOn, this.EmailAddressChangedOn);
        }

        public DateTime EmailAddressChangedOn
        {
            get => _emailAddressChangedOn;
            private set => _emailAddressChangedOn = value;
        }

        public DateTime EnablingOn
        {
            get => _enablingOn;
            private set => _enablingOn = value;
        }

        public DateTime NameChangedOn
        {
            get => _nameChangedOn;
            private set => _nameChangedOn = value;
        }
    }
}