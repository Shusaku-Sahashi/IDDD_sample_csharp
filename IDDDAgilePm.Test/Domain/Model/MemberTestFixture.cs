using System;
using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Domain.Model.Tenant;
using NUnit.Framework;

namespace IDDDAgilePm.Test.Domain.Model
{
    [TestFixture]
    public class MemberTestFixture
    {
        private readonly TenantId tenantId = new TenantId(Guid.NewGuid().ToString());
        private readonly string username = "TestUsername";
        private readonly string lastName = "Test";
        private readonly string firstName = "User";
        private readonly string emailAddress = "example.com";

        [Test]
        public void CanChangeEmailAddress()
        {
            var initializedOn = new DateTime(2020, 3, 12, 0, 0, 0);

            // Arrange
            var member = new TeamMember(
                tenantId,
                username,
                lastName,
                firstName,
                emailAddress,
                initializedOn
            );

            // Act
            var updatedOn = new DateTime(2021, 3, 12, 0, 0, 0);
            var updatedEmailAddress = "update.com";
            member.ChangeEmailAddress(updatedEmailAddress, updatedOn);

            // Assert
            Assert.That(member.Username, Is.EqualTo(username));
            Assert.That(member.TenantId, Is.EqualTo(tenantId));
            Assert.That(member.LastName, Is.EqualTo(lastName));
            Assert.That(member.FirstName, Is.EqualTo(firstName));
            Assert.That(member.EmailAddress, Is.EqualTo(updatedEmailAddress));
            Assert.That(member.ChangeTracker,
                Is.EqualTo(new MemberChangeTracker(updatedOn, initializedOn, initializedOn)));
        }

        [Test]
        public void CanChangeName()
        {
            var initializedOn = new DateTime(2020, 3, 12, 0, 0, 0);

            // Arrange
            var member = new TeamMember(
                tenantId,
                username,
                lastName,
                firstName,
                emailAddress,
                initializedOn
            );
            
            // Act
            var updatedOn = new DateTime(2021, 3, 12, 0, 0, 0);
            var updatedFirstName = "update";
            var updatedLastName = "user";
            member.ChangeName(updatedLastName,updatedFirstName, updatedOn);
            
            // Assert
            Assert.That(member.Username, Is.EqualTo(username));
            Assert.That(member.TenantId, Is.EqualTo(tenantId));
            Assert.That(member.LastName, Is.EqualTo(updatedLastName));
            Assert.That(member.FirstName, Is.EqualTo(updatedFirstName));
            Assert.That(member.ChangeTracker,
                Is.EqualTo(new MemberChangeTracker(initializedOn, initializedOn, updatedOn)));
        }

        [Test]
        public void CanSuppressChangeEmailAddress()
        {
            var initializedOn = new DateTime(2020, 3, 12, 0, 0, 0);

            // Arrange
            var member = new TeamMember(
                tenantId,
                username,
                lastName,
                firstName,
                emailAddress,
                initializedOn
            );

            // Act
            var updatedOn = initializedOn.Subtract(new TimeSpan(1, 0, 0));
            member.ChangeEmailAddress("update.com", updatedOn);

            // Assert
            Assert.That(member.Username, Is.EqualTo(username));
            Assert.That(member.TenantId, Is.EqualTo(tenantId));
            Assert.That(member.LastName, Is.EqualTo(lastName));
            Assert.That(member.FirstName, Is.EqualTo(firstName));
            Assert.That(member.EmailAddress, Is.EqualTo(emailAddress));
            Assert.That(member.ChangeTracker,
                Is.EqualTo(new MemberChangeTracker(initializedOn, initializedOn, initializedOn)));
        }
    }
}