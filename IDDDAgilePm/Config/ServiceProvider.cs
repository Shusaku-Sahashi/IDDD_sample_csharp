using System;
using IDDDAgilePm.Application;
using IDDDAgilePm.Application.Team;
using IDDDAgilePm.Domain.Model.Team;
using IDDDAgilePm.Port.Adapter.Persistence;
using IDDDCommon.Config;
using IDDDCommon.Port.Adapter.Messaging;
using IDDDCommon.Port.Adapter.Messaging.Rabbitmq;
using Microsoft.Extensions.DependencyInjection;

namespace IDDDAgilePm.Config
{
    internal class ServiceProvider
    {
        public static readonly ServiceCollection ServiceCollection;
        private static IServiceProvider _provider;

        public static IServiceProvider Current()
        {
            if (_provider != null)
            {
                return _provider;
            }
            return _provider = ServiceCollection.BuildServiceProvider();
        }

        static ServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IProductOwnerRepository, MysqlProductOwnerRepository>();
            services.AddSingleton<ITeamMemberRepository, MysqlTeamMemberRepository>();

            services.AddSingleton<ApplicationServiceLifeCycle>();
            services.AddSingleton<TeamApplicationService>();
            // TODO: 設定ファイルに外出しして、Common側で取得するように変更したい。
            services.AddSingleton(new Exchange(Exchanges.AgilePm));

            CommonServiceProvider.RegisterServices(services);

            ServiceCollection = services;
        }
    }
}