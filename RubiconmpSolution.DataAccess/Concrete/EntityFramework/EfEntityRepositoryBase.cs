using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RubiconmpSolution.Core.DataAccess;
using RubiconmpSolution.Entities.Abstract;

namespace RubiconmpSolution.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        protected readonly TContext _dbContext;

        protected EfEntityRepositoryBase(TContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = await _dbContext.Set<TEntity>().AnyAsync(filter);
            return result;
        }
    }
}