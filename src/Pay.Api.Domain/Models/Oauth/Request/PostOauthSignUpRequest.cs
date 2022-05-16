using System.ComponentModel.DataAnnotations;

namespace Pay.Api.Domain.Models.Oauth.Request
{
    public class PostOauthSignUpRequest
    {
        [Required]
		public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "E-mail invalid.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmePassword { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}