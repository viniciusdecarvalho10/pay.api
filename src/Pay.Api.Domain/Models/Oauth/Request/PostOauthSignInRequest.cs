using System.ComponentModel.DataAnnotations;

namespace Pay.Api.Domain.Models.Oauth.Request
{
    public class PostOauthSignInRequest
    {
        [Required(ErrorMessage = "User is required")]
        public string User { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}