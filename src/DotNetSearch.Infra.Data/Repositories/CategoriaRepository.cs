using Dapper;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Domain.Models;
using DotNetSearch.Infra.Data.Context;
using DotNetSearch.Infra.Data.DbConnection;
using DotNetSearch.Infra.Data.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSearch.Infra.Data.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CategoriaRepository(DotNetSearchDbContext dbContext,
                                   IDbConnectionFactory dbConnectionFactory) : base(dbContext) 
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public override async Task<IEnumerable<Categoria>> DapperSearch(SearchRequestModel searchRequestModel)
        {
            var query = new StringBuilder()
                .AppendLine("SELECT Categoria.* FROM \"Categoria\" Categoria");

            if (!string.IsNullOrEmpty(searchRequestModel.Filter))
            {
                var where = new StringBuilder().Append("WHERE ");

                var parser = new PostgreSqlFilterParser("Categoria");
                var filterParsed = parser.Parse(searchRequestModel.Filter);

                where.Append(filterParsed);
                query.AppendLine(where.ToString());
            }

            if (searchRequestModel.Sort != null && searchRequestModel.Sort.Length > 0)
            {
                var orderBy = new StringBuilder();

                foreach (var sort in searchRequestModel.Sort)
                {
                    if (string.IsNullOrEmpty(orderBy.ToString()))
                    {
                        orderBy.Append($"ORDER BY Categoria.{sort.PropertyName} {sort.Direction.GetDescription()}");
                    }
                    else
                    {
                        orderBy.Append($", Categoria.{sort.PropertyName} {sort.Direction.GetDescription()}");
                    }
                }

                query.AppendLine(orderBy.ToString());
            }

            var finalQuery = query.ToString();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Categoria>(finalQuery);
            }
        }

        public override async Task<IEnumerable<Categoria>> DapperGetAll()
        {
            var query = new StringBuilder()
                .AppendLine("SELECT * FROM \"Categoria\"")
                .ToString();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Categoria>(query);
            }
        }

        public override async Task<Categoria> DapperGetById(Guid id)
        {
            var query = new StringBuilder()
                .AppendLine("SELECT * FROM \"Categoria\" WHERE \"Id\" = @CategoriaId")
                .ToString();
            var queryParams = new { CategoriaId = id };

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Categoria>(query, queryParams);
            }
        }
    }
}
