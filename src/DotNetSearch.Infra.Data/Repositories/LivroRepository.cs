using Dapper;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Domain.Models;
using DotNetSearch.Infra.Data.Context;
using DotNetSearch.Infra.Data.DbConnection;
using Microsoft.EntityFrameworkCore;
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

        public override async Task<IEnumerable<Livro>> DapperSearch(SearchRequestModel searchRequestModel)
        {
            var query = new StringBuilder()
                .AppendLine("SELECT Livro.*, Autor.*, LivroCategoria.*, Categoria.* FROM \"Livro\" Livro")
                .AppendLine("INNER JOIN \"Autor\" Autor ON Autor.\"Id\" = Livro.\"AutorId\"")
                .AppendLine("INNER JOIN \"LivroCategoria\" LivroCategoria ON LivroCategoria.\"LivroId\" = Livro.\"Id\"")
                .AppendLine("INNER JOIN \"Categoria\" Categoria ON Categoria.\"Id\" = LivroCategoria.\"CategoriaId\"")
                .ToString();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<Livro, Autor, LivroCategoria, Categoria, Livro>(
                    query,
                    (livro, autor, livroCategoria, categoria) =>
                    {
                        livro.Autor = autor;
                        livroCategoria.Categoria = categoria;
                        livro.Categorias = new List<LivroCategoria>() { livroCategoria };
                        return livro;
                    }
                );

                var livros = result.GroupBy(g => g.Id)
                    .Select(s1 =>
                    {
                        var grouped = s1.First();
                        grouped.Categorias = s1.Select(s2 => s2.Categorias.Single()).ToList();
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
                .AppendLine("INNER JOIN \"LivroCategoria\" LivroCategoria ON LivroCategoria.\"LivroId\" = Livro.\"Id\"")
                .AppendLine("INNER JOIN \"Categoria\" Categoria ON Categoria.\"Id\" = LivroCategoria.\"CategoriaId\"")
                .ToString();

            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<Livro, Autor, LivroCategoria, Categoria, Livro>(
                    query, 
                    (livro, autor, livroCategoria, categoria) =>
                    {
                        livro.Autor = autor;
                        livroCategoria.Categoria = categoria;
                        livro.Categorias = new List<LivroCategoria>() { livroCategoria };
                        return livro;
                    }
                );

                var livros = result.GroupBy(g => g.Id)
                    .Select(s1 =>
                    {
                        var grouped = s1.First();
                        grouped.Categorias = s1.Select(s2 => s2.Categorias.Single()).ToList();
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
                .AppendLine("INNER JOIN \"LivroCategoria\" LivroCategoria ON LivroCategoria.\"LivroId\" = Livro.\"Id\"")
                .AppendLine("INNER JOIN \"Categoria\" Categoria ON Categoria.\"Id\" = LivroCategoria.\"CategoriaId\"")
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
                        livroCategoria.Categoria = categoria;
                        livro.Categorias = new List<LivroCategoria>() { livroCategoria };
                        return livro;
                    }, 
                    queryParams
                );

                var livros = result.GroupBy(g => g.Id)
                    .Select(s1 =>
                    {
                        var grouped = s1.First();
                        grouped.Categorias = s1.Select(s2 => s2.Categorias.Single()).ToList();
                        return grouped;
                    });

                return livros.SingleOrDefault();
            }
        }
    }
}
