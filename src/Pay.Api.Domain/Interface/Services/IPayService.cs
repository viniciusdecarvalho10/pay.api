using System.Threading.Tasks;
using Pay.Api.Core.Entities;
using Pay.Api.Domain.Models.Pay.Request;
using Pay.Api.Domain.Models.Pay.Response;

namespace Pay.Api.Domain.Interface.Services
{
    public interface IPayService
    {
        Task<PayResponse> PaySomething(PayRequest model, PayIdentity identity);
    }
}