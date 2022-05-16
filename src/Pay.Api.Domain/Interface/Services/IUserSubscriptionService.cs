using System;
using System.Threading.Tasks;
using Pay.Api.Domain.Entities;

namespace Pay.Api.Domain.Interface.Services
{
    public interface IUserSubscriptionService : IServiceBase<UserSubscription>
    {
        Task<UserSubscription> GetUserSubscriptionByUserId(Guid userId);
    }
}