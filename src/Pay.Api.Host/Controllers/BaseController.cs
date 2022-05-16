using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pay.Api.Host.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger _logger;
        public BaseController(ILogger logger)
        {
            _logger = logger;
        }
    }
}