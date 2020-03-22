using System;
using System.Linq;
using System.Transactions;
using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Domain.Model.Tenant;
using IDDDAgilePm.Port.Adapter.Persistence;
using NUnit.Framework;

namespace IDDDAgilePm.Test.Port.Adapter.Persistence.Mysql
{
    [TestFixture]
    public class MysqlTeamMemberRepositoryFixture
    {
        private readonly TenantId tenantId = new TenantId(Guid.NewGuid().ToString());
        private readonly string username = "TestUsername";
        private readonly string lastName = "Test";
        private readonly string firstName = "User";
        private readonly string emailAddress = "example.com";
        private readonly DateTime initializedOn = new DateTime(2020, 3, 12, 0, 0, 0);
        
        [Test]
        public void CanProvisionTeamMember()
        {
            // Arrange
            var member = new TeamMember(
                tenantId,
                username,
                lastName,
                firstName,
                emailAddress,
                initializedOn
            );
            
            var repository = new MysqlTeamMemberRepository();
            
            using var scope = new TransactionScope(TransactionScopeOption.Suppress);
            repository.Save(member);

            // Action
            var result = repository.AllTeamMembers(tenantId);
            
            // Assert
            Assert.That(result.ToList().Count, Is.EqualTo(1));

            var actual = result.First();
            
            Assert.That(actual.TenantId, Is.EqualTo(tenantId));
            Assert.That(actual.Username, Is.EqualTo(username));
            Assert.That(actual.LastName, Is.EqualTo(lastName));
            Assert.That(actual.FirstName, Is.EqualTo(firstName));
            Assert.That(actual.EmailAddress, Is.EqualTo(emailAddress));
            Assert.That(actual.ChangeTracker.EnablingOn, Is.EqualTo(initializedOn));
            Assert.That(actual.ChangeTracker.EmailAddressChangedOn, Is.EqualTo(initializedOn));
            Assert.That(actual.ChangeTracker.NameChangedOn, Is.EqualTo(initializedOn));
        }

        [Test]
        public void CanFindAllTeamMembers()
        {
            // Arrange
            var members = Enumerable.Range(0, 10).Select( n =>
                new TeamMember(
                    tenantId, 
                    username + n,
                    lastName + n,
                    firstName + n,
                    emailAddress + n,
                    initializedOn
                )
            );
            
            var repository = new MysqlTeamMemberRepository();
            repository.Save(members);
            
            // Act
            using var scope = new TransactionScope(TransactionScopeOption.Suppress);
            var actual = repository.AllTeamMembers(tenantId);
            
            // Assert
            Assert.That(actual.Count(), Is.EqualTo(10));
        }
    }
}