using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace IDDDCommon.Port.Adapter.Persistence.Mysql
{
    public sealed class MySqlProvider : DatabaseProvider
    {
        protected override DbProviderFactory Factory => MySqlClientFactory.Instance;

        // TODO: 設定ファイルからConnectionStringを取得する様に変更する。
        protected override string GetConnectionString() => "Server=127.0.0.1;Database=iddd_agile_pm;Uid=root;Pwd=password";

        public DbConnection GetConnection()
        {
            var cs = this.GetConnectionString();
            var connection = this.Factory.CreateConnection();
            
            if (connection == null) throw new InvalidOperationException("Can not obtain connection.");
            
            connection.ConnectionString = cs;
            connection.Open();
            return connection;
        }
    }
}