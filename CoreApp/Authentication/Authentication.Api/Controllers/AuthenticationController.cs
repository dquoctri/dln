using Authentication.Api.Models;
using Authentication.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _service;

        public AuthenticationController(ITokenService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserCredential payload)
        {
            Token? jwtToken = _service.CreateToken(payload);
            if (null == jwtToken)
            {
                return Unauthorized();
            }
            return Ok(jwtToken);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("refresh")]
        [HttpPost]
        public IActionResult Refresh()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Token? jwtToken = _service.CreateToken(userId);
            if (null == jwtToken)
            {
                return Unauthorized();
            }
            return Ok(jwtToken);
        }
    }
}
