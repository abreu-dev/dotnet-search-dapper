using Npgsql;

namespace DotNetSearch.Infra.Data.DbConnection
{
    public interface IDbConnectionFactory
    {
        NpgsqlConnection CreateConnection();
    }
}
