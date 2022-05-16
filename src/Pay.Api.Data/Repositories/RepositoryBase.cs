//using Confluent.Kafka;
using Pay.Api.Core.Entities;
using Pay.Api.Data.Context;
using Pay.Api.Domain.Interface.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Pay.Api.Data.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        public readonly RepositoryDbContext _dbContext;

        protected RepositoryBase(RepositoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                entity.CreateDate = DateTime.UtcNow;
                entity.DeleteDate = null;
                entity.UpdateDate = null;
                entity.UpdateUserId = null;
                entity.DeleteUserId = null;
                entity.Deleted = false;
                
                await _dbContext.Set<TEntity>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                _dbContext.Set<TEntity>().Remove(entity);
                throw;
            }
            
        }

        public virtual async Task<ICollection<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                if (predicate != null)
                {
                    var query = _dbContext.Set<TEntity>().Where(predicate);

                    return await query.ToListAsync();
                }

                return await _dbContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate != null ? _dbContext.Set<TEntity>().Where(predicate) : _dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.Set<TEntity>().FindAsync(id);  
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, Guid id)
        {
            try
            {
                if (entity == null)
                return null;

                var existing = await _dbContext.Set<TEntity>().FindAsync(id);
                if (existing == null)
                    return null;
                
                entity.UpdateDate = DateTime.UtcNow;
                entity.CreateDate = existing.CreateDate;

                _dbContext.Entry(existing).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
                return existing;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _dbContext.Set<TEntity>().Where(predicate).CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public virtual async Task<long> CountAsync()
        {
            try
            {
                return await _dbContext.Set<TEntity>().CountAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<int> DeleteAsync(object id)
        {
            try
            {
                var existing = await _dbContext.Set<TEntity>().FindAsync(id);

                if (existing == null)
                    return await Task.FromResult(0);

                _dbContext.Set<TEntity>().Remove(existing);

                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
