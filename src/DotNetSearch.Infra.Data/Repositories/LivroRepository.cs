using Dapper;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Infra.Data.Context;
using DotNetSearch.Infra.Data.DbConnection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetSearch.Infra.Data.Repositories
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public LivroRepository(DotNetSearchDbContext dbContext,
                               IDbConnectionFactory dbConnectionFactory) : base(dbContext)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        protected override IQueryable<Livro> ImproveQuery(IQueryable<Livro> query)
        {
            return query.Include(x => x.Autor)
                .Include(x => x.Categorias)
                .ThenInclude(x => x.Categoria);
        }

        public override async Task<IEnumerable<Livro>> DapperGetAll()
        {
            var query = "SELECT * FROM \"Livro\"";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Livro>(query);
            }
        }

        public override async Task<Livro> DapperGetById(Guid id)
        {
            var query = $"SELECT * FROM \"Livro\" WHERE \"Id\" = '{id}'";

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Livro>(query);
            }
        }
    }
}
