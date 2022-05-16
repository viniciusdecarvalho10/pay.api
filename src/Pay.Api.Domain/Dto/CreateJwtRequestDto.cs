using System;
using System.Collections.Generic;

namespace Pay.Api.Domain.Dto
{
    public class CreateJwtRequestDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public IDictionary<string, string> CustomClaims { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}