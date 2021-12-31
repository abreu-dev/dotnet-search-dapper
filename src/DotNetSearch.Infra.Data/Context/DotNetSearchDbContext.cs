using DotNetSearch.Domain.Entities;
using DotNetSearch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DotNetSearch.Infra.Data.Context
{
    public class DotNetSearchDbContext : DbContext, IUnitOfWork
    {
        public DotNetSearchDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Autor> Autor { get; set; }
        public DbSet<Livro> Livro { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
