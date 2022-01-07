using Dapper;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetSearch.Infra.Data.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly IConfiguration _configuration;

        public CategoriaRepository(DotNetSearchDbContext dbContext,
                                    IConfiguration configuration) : base(dbContext) 
        {
            _configuration = configuration;
        }

        public override async Task<IEnumerable<Categoria>> DapperGetAll()
        {
            var query = "SELECT * FROM \"Categoria\"";

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return await conexao.QueryAsync<Categoria>(query);
            }
        }

        public override async Task<Categoria> DapperGetById(Guid id)
        {
            var query = $"SELECT * FROM \"Categoria\" WHERE \"Id\" = '{id}'";

            using (SqlConnection conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return await conexao.QuerySingleOrDefaultAsync<Categoria>(query);
            }
        }
    }
}
