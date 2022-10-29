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
            RefreshToken? token = _service.CreateRefreshToken(payload);
            if (null == token) return Unauthorized();
            return Ok(token);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("accessToken")]
        [HttpPost]
        public IActionResult Refresh()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            AccessToken? token = _service.CreateAccessToken(userId);
            if (null == token)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
