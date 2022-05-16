using Pay.Api.Domain.Interface;
using Pay.Api.Domain.Interface.Services;
using Microsoft.Extensions.DependencyInjection;
using Pay.Api.Service.PayService;

namespace Pay.Api.Host.DependencyGroups.Entities
{
    public class PayDependencies : IDependencyGroup
    {
        /// <summary>
        /// This method is called to register dependencies with this application service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection to register with.</param>

        public void Register(IServiceCollection serviceCollection)
        {
            //services
            serviceCollection.AddTransient<IPayService, PayService>();
        }
    }
}
