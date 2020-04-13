using System;
using IDDDAgilePm.Application.Team;
using IDDDAgilePm.Config;
using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Domain.Model.Tenant;
using IDDDAgilePm.Test.Config;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServiceProvider = IDDDAgilePm.Config.ServiceProvider;

namespace IDDDAgilePm.Test.Application
{
    [TestFixture]
    internal class TeamApplicationServiceFixture
    {
        [SetUp]
        public void StartUp()
        {
            AgilePmTestServiceProvider.RegisterServices(ServiceProvider.ServiceCollection);
        }

        [Test]
        public void CanEnableProductOwner()
        {
            // Arrange
            var teamApplicationService = ServiceProvider.Current().GetRequiredService<TeamApplicationService>();
            var productOwnerRepository = ServiceProvider.Current().GetRequiredService<IProductOwnerRepository>();

            var command = new EnableProductOwnerCommand(
                Guid.NewGuid().ToString(),
                "test-user-1",
                "first-name-1",
                "last-name-1",
                "example@sample.com",
                DateTime.Parse("2020-04-12 18:00:00")
            );

            var productOwnerId = new ProductOwnerId(new TenantId(command.TenantId), command.Username);
            
            // Action
            teamApplicationService.EnableProductOwner(command);

            var productOwner = productOwnerRepository.ProductOwnerOfIdentified(productOwnerId);
            
            // Assert
            Assert.That(productOwner.ProductOwnerId(), Is.EqualTo(productOwnerId));
        }
    }
}