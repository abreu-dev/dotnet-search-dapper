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
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly BaseDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        protected Repository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        private IQueryable<TEntity> BaseQuery()
        {
            return _dbSet;
        }

        protected virtual IQueryable<TEntity> ImproveQuery(IQueryable<TEntity> query)
        {
            return query;
        }

        public virtual IQueryable<TEntity> Query()
        {
            return ImproveQuery(BaseQuery());
        }

        public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await Query()
                .Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Query().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await Query().SingleOrDefaultAsync(x => x.Id == id);
        }

        public abstract Task<IEnumerable<TEntity>> DapperGetAll();

        public abstract Task<TEntity> DapperGetById(Guid id);

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
