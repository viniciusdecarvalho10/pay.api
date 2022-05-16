using System;
using Pay.Api.Domain.Interface;
using Pay.Api.Domain.Interface.Services;
using Pay.Api.Service.DataCacheService;
using Microsoft.Extensions.DependencyInjection;

namespace Pay.Api.Host.DependencyGroups
{
    public class DataCacheDependencies : IDependencyGroup
    {
        /// <summary>
        /// This method is called to register dependencies with this application service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection to register with.</param>

        public void Register(IServiceCollection serviceCollection)
        {
            var redisHost = Environment.GetEnvironmentVariable("REDIS_HOST");
            var redisPort = Environment.GetEnvironmentVariable("REDIS_PORT");

            serviceCollection.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = $"{redisHost}:{redisPort}";
                });

            serviceCollection.AddTransient<IDataCacheService, DataCacheService>();
        }
    }
}
