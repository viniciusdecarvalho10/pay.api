using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pay.Api.Domain.Interface;
using Pay.Api.Data.Context;

namespace Pay.Api.DependencyGroups
{
    public class SqlServerDependencies : IDependencyGroup
    {
        public void Register(IServiceCollection services)
        {
            var SQL_CONNECTION = Environment.GetEnvironmentVariable("SQL_CONNECTION");

            services.AddDbContext<RepositoryDbContext>(options =>
            {
                options
                   .UseSqlServer(SQL_CONNECTION, providerOptions => providerOptions
                   .EnableRetryOnFailure(3)
                   .CommandTimeout(30)
               )
               .UseLazyLoadingProxies(true)
               .EnableDetailedErrors();
            });
        }
    }
}