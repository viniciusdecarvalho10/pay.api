using System;
using System.Threading.Tasks;
using Pay.Api.Domain.Interface.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Pay.Api.Service.DataCacheService
{
    public class DataCacheService : IDataCacheService
    {
        private readonly IDistributedCache _cache;

        public DataCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<T> GetAsync<T>(string key)
        {
            var async = _cache.GetString(key);
            if (string.IsNullOrEmpty(async))
                return await Task.FromResult(default(T));

            return await Task.FromResult((T)JsonConvert.DeserializeObject<T>(async));
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getDataCallback) where T : class
        {
            T data = await GetAsync<T>(key) as T;
            if (data == null)
            {
                data = await getDataCallback();
                await SetAsync<T>(key, data);
            }
            return data;
        }

        public Task Remove(string key)
        {
            return _cache.RemoveAsync(key);
        }

        public Task SetAsync<T>(string key, T obj, TimeSpan? expires = null)
        {
            string string_value = JsonConvert.SerializeObject(obj);

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expires ?? new TimeSpan(1)
            };

            return _cache.SetStringAsync(key, string_value, options);
        }
    }
}