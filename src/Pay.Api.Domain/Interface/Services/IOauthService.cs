using System.Threading.Tasks;
using Pay.Api.Domain.Models.Oauth.Request;
using Pay.Api.Domain.Models.Oauth.Response;

namespace Pay.Api.Domain.Interface.Services
{
    public interface IOauthService
    {
        Task<PostOauthSignUpResponse> SignUp(PostOauthSignUpRequest model);
        Task<OauthTokenResponse> SignIn(PostOauthSignInRequest model);
        Task<OauthTokenResponse> RefreshToken(PostOauthRefreshTokenRequest model);
    }
}