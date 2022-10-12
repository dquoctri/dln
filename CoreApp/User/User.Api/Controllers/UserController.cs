using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("validate")]
        [HttpGet]
        public IActionResult Validate()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            _logger.Log(LogLevel.Information, "Validate JwtBearerDefaults.AuthenticationScheme ");
            return Ok();
        }
    }
}
