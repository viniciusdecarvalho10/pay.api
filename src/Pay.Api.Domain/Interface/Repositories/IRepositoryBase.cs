using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pay.Api.Domain.Interface.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, Guid id);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<long> CountAsync();
        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> DeleteAsync(object id);
    }
}