using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Pay.Api.Core.Entities;
using Pay.Api.Core.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Pay.Api.Host.Controllers
{
    public class AuthBaseController : BaseController
    {
        public PayIdentity PayIdentity { get; set; }
        public AuthBaseController(ILogger logger) : base(logger)
        {
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                Parallel.Invoke(
                    () => this.LoadIdentity()
                );
            }
            catch (Exception ex)
            {
                this._logger.LogWarning(ex, "{0}.OnActionExecuting", this.GetType().Name);
            }
        }

        private void LoadIdentity()
        {
            var identity = (ClaimsIdentity)User.Identity;

            IEnumerable<Claim> claims = identity.Claims;

            try
            {
                PayIdentity = identity.GetIdentity();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}