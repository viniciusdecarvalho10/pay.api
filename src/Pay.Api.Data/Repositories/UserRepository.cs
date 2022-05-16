using Pay.Api.Data.Context;
using Pay.Api.Domain.Entities;
using Pay.Api.Domain.Interface.Repositories;

namespace Pay.Api.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        readonly RepositoryDbContext _repositoryDbContext;

        public UserRepository(RepositoryDbContext repositoryDbContext) : base(repositoryDbContext)
        {
            _repositoryDbContext = repositoryDbContext;
        }
    }
}