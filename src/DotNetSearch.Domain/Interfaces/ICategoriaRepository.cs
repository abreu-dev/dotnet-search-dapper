using DotNetSearch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetSearch.Domain.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria> 
    {
        Task<IEnumerable<Categoria>> CustomSearch(int page,
                                                  int pageSize,
                                                  Expression<Func<Categoria, bool>> predicate);

        Task<IEnumerable<Categoria>> DapperSearch(string query);
    }
}
