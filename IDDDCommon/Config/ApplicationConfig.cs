using System.IO;
using Microsoft.Extensions.Configuration;

namespace IDDDCommon.Config
{
    public class ApplicationConfig
    {
        private static IConfiguration Configuration { get; set; }

        public static IConfiguration Current()
        {
            return Configuration ??= new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // exeファイルがある場所を指定するため。
                .AddJsonFile("appsettings.json", optional:true) // appsettings.jsonをJsonの設定ファイルとして追加
                .Build();
        }
    }
}