using System;
using System.Linq;
using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Domain.Model.Tenant;
using NUnit.Framework;

namespace IDDDAgilePm.Test.Domain.Model
{
    [TestFixture]
    public class TeamTestFixture
    {
        private readonly TenantId tenantId = new TenantId(Guid.NewGuid().ToString());
        private readonly string username = "username";
        private readonly string firstName = "first";
        private readonly string lastName = "last";
        private readonly string emailAddress = "example.com";
        private readonly string teamName = "team name";

        [Test]
        public void CanAssignProductOwner()
        {
            // Arrange
            var productOwner = new ProductOwner(tenantId,
                username,
                lastName,
                firstName,
                emailAddress,
                new DateTime(2020, 3, 10, 0, 0, 0));
            var team = new Team(tenantId, teamName);

            // Act
            team.AssignProductOwner(productOwner);

            // Assert
            Assert.That(team.ProductOwner, Is.EqualTo(productOwner));
        }

        [Test]
        public void CanRemoveTeamMember()
        {
            // Arrange
            var team = new Team(tenantId, teamName);

            TeamMember teamMember = null;
            foreach (var n in Enumerable.Range(0, 10))
            {
                teamMember = new TeamMember(tenantId, $"username{n}", $"lastName{n}", $"firstName{n}",
                    $"emailAddress{n}", DateTime.Now);
                team.AssignTeamMember(teamMember);
            }

            // Act
            team.RemoveTeamMember(teamMember);
            
            // Assert
            Assert.That(team.AllTeamMembers().Count, Is.EqualTo(9));
        }
    }
}