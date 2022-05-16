

namespace Pay.Api.Domain.Models.Oauth.Response
{
    public class OauthTokenResponse
    {
		public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresIn { get; set; }
        public long RefreshTokenExpiresIn { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}