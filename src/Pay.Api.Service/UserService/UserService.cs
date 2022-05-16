using System.Linq;
using System.Threading.Tasks;
using Pay.Api.Domain.Entities;
using Pay.Api.Domain.Interface.Repositories;
using Pay.Api.Domain.Interface.Services;

namespace Pay.Api.Service.UserService
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<User> GetByEmail(string email)
        {
            return (await base.FindByAsync(u => u.Email == email)).FirstOrDefault();
        }
    }
}