using Pay.Api.Data.Context;
using Pay.Api.Domain.Entities;
using Pay.Api.Domain.Interface.Repositories;

namespace Pay.Api.Data.Repositories
{
    public class UserSubscriptionRepository : RepositoryBase<UserSubscription>, IUserSubscriptionRepository
    {
        readonly RepositoryDbContext _repositoryDbContext;

        public UserSubscriptionRepository(RepositoryDbContext repositoryDbContext) : base(repositoryDbContext)
        {
            _repositoryDbContext = repositoryDbContext;
        }
    }
}