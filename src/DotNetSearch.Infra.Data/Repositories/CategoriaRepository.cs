using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using DotNetSearch.Infra.Data.Context;
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
                .ToListAsync();
        }
    }
}
