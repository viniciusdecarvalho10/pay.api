using System.ComponentModel.DataAnnotations;

namespace Pay.Api.Domain.Models.Pay.Request
{
    public class PayRequest
    {
        [Required(ErrorMessage = "Amount is required")]
        [Display(Name = "Amount", Description = "Enter between 0.1 and 99999.")]
        [Range(0.1, 99999)]
        public double Amount { get; set; }
        [Required(ErrorMessage = "EmailReceive is required")]
        public string EmailReceive { get; set; }
    }
}