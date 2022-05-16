using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Pay.Api.Domain.Dto;
using Pay.Api.Domain.Interface.Services;
using Microsoft.IdentityModel.Tokens;

namespace Pay.Api.Service.CreateJwtService
{
    public class CreateJwtService : ICreateJwtService
    {
        public async Task<SecurityToken> Execute(CreateJwtRequestDto createJwtRequest)
        {
            var claims = new List<Claim>{
                        new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ("N")),
                        new Claim (JwtRegisteredClaimNames.UniqueName, createJwtRequest.Username),
            };

            if (createJwtRequest.CustomClaims != null && createJwtRequest.CustomClaims.Any())
            {
                var customClaims = createJwtRequest.CustomClaims.Select(x => new Claim(x.Key, x.Value));
                claims.AddRange(customClaims);
            }

            var identity = new ClaimsIdentity(new GenericIdentity(createJwtRequest.UserId.ToString(), "identity_id"), claims);
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")));
            var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                SigningCredentials = creeds,
                Subject = identity,
                NotBefore = createJwtRequest.CreatedDate,
                Expires = createJwtRequest.ExpirationDate
            });


            return await Task.FromResult(securityToken);
        }
    }
}