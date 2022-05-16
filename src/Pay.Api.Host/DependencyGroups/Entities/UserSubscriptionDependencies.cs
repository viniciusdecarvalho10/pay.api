using Pay.Api.Data.Repositories;
using Pay.Api.Domain.Interface;
using Pay.Api.Domain.Interface.Repositories;
using Pay.Api.Domain.Interface.Services;
using Pay.Api.Service.UserSubscriptionService;
using Microsoft.Extensions.DependencyInjection;

namespace Pay.Api.Host.DependencyGroups.Entities
{
    public class UserSubscriptionDependencies : IDependencyGroup
    {
        /// <summary>
        /// This method is called to register dependencies with this application service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection to register with.</param>

        public void Register(IServiceCollection serviceCollection)
        {
            //repositories
            serviceCollection.AddTransient<IUserSubscriptionRepository, UserSubscriptionRepository>();

            //services
            serviceCollection.AddTransient<IUserSubscriptionService, UserSubscriptionService>();
        }
    }
}
