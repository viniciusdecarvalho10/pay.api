using System;
using System.Linq;
using System.Threading.Tasks;
using Pay.Api.Domain.Entities;
using Pay.Api.Domain.Interface.Repositories;
using Pay.Api.Domain.Interface.Services;

namespace Pay.Api.Service.UserSubscriptionService
{
    public class UserSubscriptionService : ServiceBase<UserSubscription>, IUserSubscriptionService
    {
        private readonly IUserSubscriptionRepository _repository;

        public UserSubscriptionService(IUserSubscriptionRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<UserSubscription> GetUserSubscriptionByUserId(Guid userId)
        {
            return (await base.FindByAsync(c => c.UserId == userId)).FirstOrDefault();
        }
    }
}