using Pay.Api.Data.Context;
using Pay.Api.Domain.Entities;
using Pay.Api.Domain.Interface.Repositories;

namespace Pay.Api.Data.Repositories
{
    public class SubscriptionRepository : RepositoryBase<Subscription>, ISubscriptionRepository
    {
        readonly RepositoryDbContext _repositoryDbContext;

        public SubscriptionRepository(RepositoryDbContext repositoryDbContext) : base(repositoryDbContext)
        {
            _repositoryDbContext = repositoryDbContext;
        }
    }
}