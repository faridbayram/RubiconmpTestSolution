
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RubiconmpSolution.Entities.Abstract;

namespace RubiconmpSolution.Core.DataAccess
{
    public interface IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<int> CreateAsync(TEntity entity);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
    }
}