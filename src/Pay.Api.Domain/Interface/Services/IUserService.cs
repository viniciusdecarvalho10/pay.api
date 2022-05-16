using System.Threading.Tasks;
using Pay.Api.Domain.Entities;

namespace Pay.Api.Domain.Interface.Services
{
    public interface IUserService : IServiceBase<User>
    {
        Task<User> GetByEmail(string email);
    }
}