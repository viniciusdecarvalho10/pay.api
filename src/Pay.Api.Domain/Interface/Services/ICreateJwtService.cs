using System.Threading.Tasks;
using Pay.Api.Domain.Dto;
using Microsoft.IdentityModel.Tokens;

namespace Pay.Api.Domain.Interface.Services
{
    public interface ICreateJwtService
    {
        Task<SecurityToken> Execute(CreateJwtRequestDto createJwtRequest);
    }
}