using Pay.Api.Domain.Interface;
using Pay.Api.Domain.Interface.Services;
using Pay.Api.Service.CreateJwtService;
using Pay.Api.Service.OauthService;
using Microsoft.Extensions.DependencyInjection;

namespace Pay.Api.Host.DependencyGroups
{
    public class OauthDependencies : IDependencyGroup
    {
        /// <summary>
        /// This method is called to register dependencies with this application service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection to register with.</param>

        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IOauthService, OauthService>();
            serviceCollection.AddTransient<ICreateJwtService, CreateJwtService>();
        }
    }
}
