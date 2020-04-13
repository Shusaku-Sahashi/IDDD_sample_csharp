using System.IO;
using Microsoft.Extensions.Configuration;

namespace IDDDAgilePm.Config
{
    internal class AgilePmConfigure
    {
        private static IConfiguration Configuration { get; set; }

        public static IConfiguration Current()
        {
            return Configuration ??= new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // exeファイルがある場所を指定するため。
                .AddJsonFile("appsettings.json", optional:true) // appsettings.jsonをJsonの設定ファイルとして追加
                .Build();
        }

        public static void StartUp()
        {
            // DIコンテナを設定する。
            ServiceProvider.Current();
            
            // Listenerの設定
            DomainEventListenerStart();
            
            // StoreEventのPublisherの起動
            StartEventPublisherTimerStart();
        }

        private static void DomainEventListenerStart()
        {
            
        }

        private static void StartEventPublisherTimerStart()
        {
            
        }
    }
}