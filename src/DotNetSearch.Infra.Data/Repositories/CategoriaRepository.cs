using Dapper;
using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Infra.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetSearch.Infra.Data.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(DotNetSearchDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Categoria>> CustomSearch(int page, 
                                                         int pageSize, 
                                                         Expression<Func<Categoria, bool>> predicate)
        {
            return await Query()
                .Where(predicate)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Categoria>> DapperSearch(string query)
        {
            using (SqlConnection conexao = 
                new SqlConnection("Server=localhost,1433;Database=DotNetSearch;User ID=sa;Password=RfAjiY5LL5"))
            {
                return await conexao.QueryAsync<Categoria>(
                    "SELECT * FROM Categoria C");
            }
        }
    }
}
