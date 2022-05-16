using System;
using System.Threading.Tasks;

namespace Pay.Api.Domain.Interface.Services
{
    public interface IDataCacheService
    {
        Task SetAsync<T>(string key, T obj, TimeSpan? expires = null);
        Task<T> GetAsync<T>(string key);
        Task Remove(string key);
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getDataCallback) where T : class;
    }
}