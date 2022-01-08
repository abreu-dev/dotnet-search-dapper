using Npgsql;
using System.Data;

namespace DotNetSearch.Infra.Data.DbConnection
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string dbConnectionString;

        public SqlConnectionFactory(string dbConnectionString)
        {
            this.dbConnectionString = dbConnectionString;
        }

        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(dbConnectionString);
        }
    }
}
