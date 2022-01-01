using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetSearch.Infra.Data.Context
{
    public interface IDbContext : IUnitOfWork
    {
        DbSet<T> Set<T>() where T : class;
    }
}
