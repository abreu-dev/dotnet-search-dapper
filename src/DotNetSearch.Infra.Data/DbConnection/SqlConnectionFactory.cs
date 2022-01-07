using Microsoft.Data.SqlClient;
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

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(dbConnectionString);
        }
    }
}
