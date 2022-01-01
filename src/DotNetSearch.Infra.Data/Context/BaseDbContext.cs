using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DotNetSearch.Infra.Data.Context
{
    public abstract class BaseDbContext : DbContext, IDbContext
    {
        protected BaseDbContext(DbContextOptions options) : base(options) { }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}
