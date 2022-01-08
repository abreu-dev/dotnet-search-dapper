using Dapper;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Enums;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Domain.Models;
using DotNetSearch.Infra.Data.Context;
using DotNetSearch.Infra.Data.DbConnection;
using DotNetSearch.Infra.Data.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .AppendLine("SELECT Categoria.* FROM \"Categoria\" Categoria WHERE 1=1");

            var parser = new PostgreSqlFilterParser("Categoria");

            if (searchRequestModel.LastRow != null)
            {
                var jsonObject = JObject.Parse(Convert.ToString(searchRequestModel.LastRow));
                var jsonProperties = jsonObject.Descendants().Where(property => property is JValue);
                var lastRowFilter = new Dictionary<string, string>();

                foreach (var sort in searchRequestModel.Sort)
                {
                    var value = jsonProperties.FirstOrDefault(property => property.Path == sort.PropertyName);
                    if (value != null)
                    {
                        var propertyPath = parser.BuildPropertyPath(sort.PropertyName);
                        var propertyValue = $"'{value}'";
                        lastRowFilter.Add(propertyPath, propertyValue);
                    }
                }

                if (lastRowFilter.Count > 0)
                {
                    var where = new StringBuilder()
                        .Append("AND ")
                        .Append(string.Format("({0}) {1} ({2})",
                        string.Join(",", lastRowFilter.Keys),
                        searchRequestModel.Sort.First().Direction == SearchSortDirection.Desc ? "<" : ">",
                        string.Join(",", lastRowFilter.Values)));
                    query.AppendLine(where.ToString());
                }
            }

            if (!string.IsNullOrEmpty(searchRequestModel.Filter))
            {
                var where = new StringBuilder().Append("AND ");

                var filterParsed = parser.Parse(searchRequestModel.Filter);

                where.Append(filterParsed);
                query.AppendLine(where.ToString());
            }

            if (searchRequestModel.Sort != null && searchRequestModel.Sort.Length > 0)
            {
                var orderBy = new StringBuilder();

                foreach (var sort in searchRequestModel.Sort)
                {
                    orderBy.Append(
                        string.Format("{0} {1} {2}",
                            orderBy.Length > 0 ? "," : "ORDER BY",
                            parser.BuildPropertyPath(sort.PropertyName),
                            sort.Direction.GetDescription()));
                }

                query.AppendLine(orderBy.ToString());
            }

            if (searchRequestModel.PageSize > 0)
            {
                query.AppendLine(string.Format("LIMIT {0}", searchRequestModel.PageSize));
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
