using DotNetSearch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetSearch.Infra.Data.Context
{
    public class DotNetSearchDbContext : BaseDbContext
    {
        public DotNetSearchDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Autor> Autor { get; set; }
        public DbSet<Livro> Livro { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}
