using System.Data;

namespace DotNetSearch.Infra.Data.DbConnection
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
