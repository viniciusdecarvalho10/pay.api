

using System.ComponentModel.DataAnnotations;

namespace Pay.Api.Domain.Models.Pay.Response
{
    public class PayResponse
    {
		public string TransactionToken { get; set; }
        public string NameReceive { get; set; }
        public string EmailReceive { get; set; }
        public string NameSender { get; set; }
        public string Message { get; set; }
        public bool Successful { get; set; }
        public double Amount { get; set; }
    }
}