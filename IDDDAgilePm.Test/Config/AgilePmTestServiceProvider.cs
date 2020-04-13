using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Test.Port.Adapter.Persistence.Inmemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IDDDAgilePm.Test.Config
{
    public class AgilePmTestServiceProvider
    {
        public static void RegisterServices(ServiceCollection services)
        {
            // テスト用にクラスの切り替えを行う。
            services.Replace(ServiceDescriptor.Singleton<IProductOwnerRepository, InMemoryProductOwnerRepository>());
            services.Replace(ServiceDescriptor.Singleton<ITeamMemberRepository, InMemoryTeamMemberRepository>());
        }

        private AgilePmTestServiceProvider() { }
    }
}