using System.ComponentModel.DataAnnotations;

namespace Pay.Api.Domain.Models.Oauth.Request
{
    public class PostOauthRefreshTokenRequest
    {
        [Required(ErrorMessage = "RefreshToken is require.")]
        public string RefreshToken { get; set; }
    }
}