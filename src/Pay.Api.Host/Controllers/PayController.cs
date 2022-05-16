using System.Threading.Tasks;
using Pay.Api.Core.Entities;
using Pay.Api.Domain.Interface.Services;
using Pay.Api.Domain.Models.Pay.Request;
using Pay.Api.Domain.Models.Pay.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pay.Api.Host.Controllers
{
    [ApiController]
    [Route("pay")]
    public class PayController : AuthBaseController
    {

        public PayController(ILogger<PayController> logger) : base(logger)
        {
        }


        [Authorize]
        [HttpPost()]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PayResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PaySomething([FromServices] IPayService service, [FromBody] PayRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var result = await service.PaySomething(model, this.PayIdentity);

            return Ok(result);
        }
    }
}