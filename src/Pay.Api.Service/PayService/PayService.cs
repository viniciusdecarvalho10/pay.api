using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Pay.Api.Core.Entities;
using Pay.Api.Domain.Interface.Services;
using Pay.Api.Domain.Models.Pay.Request;
using Pay.Api.Domain.Models.Pay.Response;

namespace Pay.Api.Service.PayService
{
    public class PayService : IPayService
    {
        private readonly IUserService _userService;

        public PayService(IUserService userService) : base()
        {
            _userService = userService;
        }

        public async Task<PayResponse> PaySomething(PayRequest model, PayIdentity identity)
        {
            if (identity.UserEmail == model.EmailReceive)
            {
                throw new ValidationException("You can't pay yourself!");
            }

            var user = await _userService.GetByIdAsync(identity.UserId);
            var userReceive = await _userService.GetByEmail(model.EmailReceive);

            if (userReceive == null)
            {
                throw new ValidationException("Email Receive invalid.");
            }

            var result = new PayResponse {
                EmailReceive = userReceive.Email,
                NameReceive = userReceive.Name,
                NameSender = user.Name,
                Successful = model.Amount < 500 ? true : false,
                Message = model.Amount < 500 ? "Successful" : "Limit exceeded",
                Amount = model.Amount,
                TransactionToken = GetTransactionToken($"EmailReceive: {userReceive.Email} NameReceive: {userReceive.Name} NameSender: {user.Name} EmailSender:{user.Email} Amount:{model.Amount} Date:{DateTime.UtcNow}")
            };
            return await Task.FromResult(result);
        }

        private string GetTransactionToken(string data)
        {
            byte[] dataAsBytes = Encoding.ASCII.GetBytes(data);
            var result = System.Convert.ToBase64String(dataAsBytes);
            return result;
        }
    }
}