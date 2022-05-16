using Pay.Api.Domain.Entities;
using Pay.Api.Domain.Interface.Repositories;
using Pay.Api.Domain.Interface.Services;

namespace Pay.Api.Service.SubscriptionService
{
    public class SubscriptionService : ServiceBase<Subscription>, ISubscriptionService
    {
        private readonly ISubscriptionRepository _repository;

        public SubscriptionService(ISubscriptionRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}