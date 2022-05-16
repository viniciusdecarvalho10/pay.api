using Pay.Api.Core.Entities;

namespace Pay.Api.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool EmailVerified { get; set; }
    }
}