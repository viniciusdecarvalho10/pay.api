using System;
using System.ComponentModel.DataAnnotations;
using Pay.Api.Core.Entities;

namespace Pay.Api.Domain.Entities
{
    public class Subscription : BaseEntity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Address { get; set; } 
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}