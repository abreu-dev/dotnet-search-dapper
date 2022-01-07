using Dapper;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Infra.Data.Context;
using DotNetSearch.Infra.Data.DbConnection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.Infra.Data.Repositories
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public AutorRepository(DotNetSearchDbContext dbContext,
                               IDbConnectionFactory dbConnectionFactory) : base(dbContext)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public override async Task<IEnumerable<Autor>> DapperGetAll()
        {
            var query = "SELECT * FROM \"Autor\"";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Autor>(query);
            }
        }

        public override async Task<Autor> DapperGetById(Guid id)
        {
            var query = $"SELECT * FROM \"Autor\" WHERE \"Id\" = '{id}'";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Autor>(query);
            }
        }
    }
}
