using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pay.Api.Domain.Interface.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, Guid id);
        Task<int> DeleteAsync(Guid id);
        Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
    }
}