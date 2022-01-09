using Dapper;
using DotNetSearch.Domain.Common;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Enums;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Domain.Models;
using DotNetSearch.Infra.Data.Context;
using DotNetSearch.Infra.Data.DbConnection;
using DotNetSearch.Infra.Data.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private string BuildOrderByStatement(SearchSortModel[] searchSortModel, PostgreSqlFilterParser parser)
        {
            var orderBy = new StringBuilder();

            foreach (var sort in searchSortModel)
            {
                orderBy.Append(
                    string.Format("{0} {1} {2}",
                        orderBy.Length > 0 ? "," : "ORDER BY",
                        parser.BuildPropertyPath(sort.PropertyName),
                        sort.Direction.GetDescription()
                    )
                );
            }

            return orderBy.ToString();
        }

        private string BuildPaginationWhereStatement(object lastRow, SearchSortModel[] searchSortModel, PostgreSqlFilterParser parser)
        {
            var jsonObject = JObject.Parse(Convert.ToString(lastRow));
            var jsonProperties = jsonObject.Descendants().Where(property => property is JValue);
            var lastRowFilter = new Dictionary<string, string>();

            foreach (var sort in searchSortModel)
            {
                var value = jsonProperties.FirstOrDefault(property => property.Path == sort.PropertyName);
                if (value != null)
                {
                    var propertyPath = parser.BuildPropertyPath(sort.PropertyName);
                    var propertyValue = $"'{value}'";
                    lastRowFilter.Add(propertyPath, propertyValue);
                }
            }

            var where = new StringBuilder();

            if (lastRowFilter.Count > 0)
            {
                where.Append(
                    string.Format("AND ({0}) {1} ({2})",
                        string.Join(",", lastRowFilter.Keys),
                        searchSortModel.First().Direction == SearchSortDirection.Desc ? "<" : ">",
                        string.Join(",", lastRowFilter.Values)
                    )
                );
            }

            return where.ToString();
        }

        private string BuildPaginationLimitStatement(int pageSize)
        {
            return string.Format("LIMIT {0}", pageSize);
        }

        private string BuildWhereFilterStatement(string filter, PostgreSqlFilterParser parser)
        {
            var filterParsed = parser.Parse(filter);
            return new StringBuilder()
                .Append("AND (")
                .Append(filterParsed)
                .Append(")")
                .ToString();
        }

        public override async Task<IEnumerable<Livro>> DapperSearch(SearchRequestModel searchRequestModel)
        {
            var baseQuery = new StringBuilder()
                .AppendLine("SELECT Livro.*, Autor.*, LivroCategoria.*, Categoria.* FROM \"Livro\" Livro")
                .AppendLine("INNER JOIN \"Autor\" Autor ON Autor.\"Id\" = Livro.\"AutorId\"")
                .AppendLine("LEFT JOIN \"LivroCategoria\" LivroCategoria ON LivroCategoria.\"LivroId\" = Livro.\"Id\"")
                .AppendLine("LEFT JOIN \"Categoria\" Categoria ON Categoria.\"Id\" = LivroCategoria.\"CategoriaId\"")
                .AppendLine("WHERE 1=1");

            var parser = new PostgreSqlFilterParser("Livro");

            if (searchRequestModel.Sort == null || searchRequestModel.Sort.Length <= 0)
            {
                searchRequestModel.Sort = new SearchSortModel[]
                {
                    new SearchSortModel()
                    {
                        PropertyName = nameof(Livro.Titulo).ToLower(),
                        Direction = SearchSortDirection.Asc
                    }
                }.ToArray();
            }

            if (searchRequestModel.LastRow != null)
            {
                var paginationWhereStatement = BuildPaginationWhereStatement(searchRequestModel.LastRow, searchRequestModel.Sort, parser);
                baseQuery.AppendLine(paginationWhereStatement);
            }

            if (!string.IsNullOrEmpty(searchRequestModel.Filter))
            {
                var whereFilterStatement = BuildWhereFilterStatement(searchRequestModel.Filter, parser);
                baseQuery.AppendLine(whereFilterStatement);
            }

            var orderByStatement = BuildOrderByStatement(searchRequestModel.Sort, parser);
            baseQuery.AppendLine(orderByStatement);

            if (searchRequestModel.PageSize > 0)
            {
                var paginationLimitStatement = BuildPaginationLimitStatement(searchRequestModel.PageSize);
                baseQuery.AppendLine(paginationLimitStatement);
            }

            var finalQuery = baseQuery.ToString();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<Livro, Autor, LivroCategoria, Categoria, Livro>(
                    finalQuery,
                    (livro, autor, livroCategoria, categoria) =>
                    {
                        livro.Autor = autor;
                        livro.Categorias = new List<LivroCategoria>();

                        if (livroCategoria != null)
                        {
                            livroCategoria.Categoria = categoria;
                            livro.Categorias.Add(livroCategoria);
                        }

                        return livro;
                    }
                );

                var livros = result.GroupBy(g => g.Id)
                    .Select(s1 =>
                    {
                        var grouped = s1.First();
                        grouped.Categorias = s1.Where(w1 => w1.Categorias.Any()).Select(s2 => s2.Categorias.Single()).ToList();
                        return grouped;
                    });

                return livros;
            }
        }

        public override async Task<IEnumerable<Livro>> DapperGetAll()
        {
            var query = new StringBuilder()
                .AppendLine("SELECT Livro.*, Autor.*, LivroCategoria.*, Categoria.* FROM \"Livro\" Livro")
                .AppendLine("INNER JOIN \"Autor\" Autor ON Autor.\"Id\" = Livro.\"AutorId\"")
                .AppendLine("LEFT JOIN \"LivroCategoria\" LivroCategoria ON LivroCategoria.\"LivroId\" = Livro.\"Id\"")
                .AppendLine("LEFT JOIN \"Categoria\" Categoria ON Categoria.\"Id\" = LivroCategoria.\"CategoriaId\"")
                .ToString();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<Livro, Autor, LivroCategoria, Categoria, Livro>(
                    query, 
                    (livro, autor, livroCategoria, categoria) =>
                    {
                        livro.Autor = autor;
                        livro.Categorias = new List<LivroCategoria>();

                        if (livroCategoria != null)
                        {
                            livroCategoria.Categoria = categoria;
                            livro.Categorias.Add(livroCategoria);
                        }

                        return livro;
                    }
                );

                var livros = result.GroupBy(g => g.Id)
                    .Select(s1 =>
                    {
                        var grouped = s1.First();
                        grouped.Categorias = s1.Where(w1 => w1.Categorias.Any()).Select(s2 => s2.Categorias.Single()).ToList();
                        return grouped;
                    });

                return livros;
            }
        }

        public override async Task<Livro> DapperGetById(Guid id)
        {
            var query = new StringBuilder()
                .AppendLine("SELECT Livro.*, Autor.*, LivroCategoria.*, Categoria.* FROM \"Livro\" Livro")
                .AppendLine("INNER JOIN \"Autor\" Autor ON Autor.\"Id\" = Livro.\"AutorId\"")
                .AppendLine("LEFT JOIN \"LivroCategoria\" LivroCategoria ON LivroCategoria.\"LivroId\" = Livro.\"Id\"")
                .AppendLine("LEFT JOIN \"Categoria\" Categoria ON Categoria.\"Id\" = LivroCategoria.\"CategoriaId\"")
                .AppendLine("WHERE Livro.\"Id\" = @LivroId")
                .ToString();
            var queryParams = new { LivroId = id };

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<Livro, Autor, LivroCategoria, Categoria, Livro>(
                    query, 
                    (livro, autor, livroCategoria, categoria) =>
                    {
                        livro.Autor = autor;
                        livro.Categorias = new List<LivroCategoria>();

                        if (livroCategoria != null)
                        {
                            livroCategoria.Categoria = categoria;
                            livro.Categorias.Add(livroCategoria);
                        }

                        return livro;
                    }, 
                    queryParams
                );

                var livros = result.GroupBy(g => g.Id)
                    .Select(s1 =>
                    {
                        var grouped = s1.First();
                        grouped.Categorias = s1.Where(w1 => w1.Categorias.Any()).Select(s2 => s2.Categorias.Single()).ToList();
                        return grouped;
                    });

                return livros.SingleOrDefault();
            }
        }
    }
}
