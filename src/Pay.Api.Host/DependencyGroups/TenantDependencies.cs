using Pay.Api.Domain.Interface;
using Pay.Api.Domain.Interface.Services;
using Pay.Api.Service.TenantService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Pay.Api.Host.DependencyGroups
{
    public class TenantDependencies : IDependencyGroup
    {
        /// <summary>
        /// This method is called to register dependencies with this application service collection.
        /// </summary>
        /// <param name="services">The service collection to register with.</param>

        public void Register(IServiceCollection services)
        {
            services.AddTransient<ITenantService, TenantService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
