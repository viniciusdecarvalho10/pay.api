using System.Threading.Tasks;
using Pay.Api.Core.Entities;
using Pay.Api.Domain.Interface.Services;
using Pay.Api.Domain.Models.Oauth.Request;
using Pay.Api.Domain.Models.Oauth.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pay.Api.Host.Controllers
{
    [ApiController]
    [Route("oauth")]
    public class OauthController : BaseController
    {

        public OauthController(ILogger<OauthController> logger) : base(logger)
        {
        }


        [AllowAnonymous]
        [HttpPost("signin")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(OauthTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SignIn([FromServices] IOauthService service, [FromBody] PostOauthSignInRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var result = await service.SignIn(model);
            return Ok(result);
        }

        [HttpPost("refreshtoken")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(OauthTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshTokenAsync([FromServices] IOauthService service, [FromBody] PostOauthRefreshTokenRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var result = await service.RefreshToken(model);
            return Ok(result);
        }

        /// <summary>
        /// Create Usuario
        /// </summary>
        /// <param name="oauthService"></param>
        /// <param name="model">Oauth to create</param>
        /// <returns>The Businesses</returns>
        /// <response code="201">Created - The request has succeeded and a new resource has been created as a result of it. This is typically the response sent after a POST request, or after some PUT requests.</response>
        /// <response code="400">Bad Request – This means that client-side input fails validation.</response>
        /// <response code="401">Unauthorized – This means the user isn’t not authorized to access a resource. It usually returns when the user isn’t authenticated.</response>
        /// <response code="403">Forbidden – This means the user is authenticated, but it’s not allowed to access a resource.</response>
        /// <response code="412">Precondition Failed - The client has indicated preconditions in its headers which the server does not meet.</response>
        /// <response code="422">Unprocessable Entity - The request was well-formed but was unable to be followed due to semantic errors.</response>
        /// <response code="500">Intertal Server Error - The server has encountered a situation it doesn't know how to handle.</response>
        [HttpPost("signup")]
        [ProducesResponseType(typeof(PostOauthSignUpResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]

        public async Task<ActionResult> SignUp([FromServices] IOauthService oauthService, [FromBody] PostOauthSignUpRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await oauthService.SignUp(model);
                return Created("Post", result);
            }
            return BadRequest(ModelState);
        }
    }
}