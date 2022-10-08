using Authentication.Service;
using Authentication.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public ActionResult<object> Login([FromBody] LoginRequest payload)
        {
            var username = payload.Username;
            var password = payload.Password;
            var token = _service.Login(new LoginRequest(username, password));
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return token;
        }

        [Authorize]
        [Route("logout")]
        [HttpPost]
        public ActionResult<string> Logout()
        {
            //await _tokenManager.DeactivateCurrentAsync();

            return NoContent();
        }


        [Authorize(Roles = "Adminitrator")]
        [Route("cancelToken")]
        [HttpPost]
        public ActionResult<string> CancelToken()
        {
            //await _tokenManager.DeactivateCurrentAsync();

            return NoContent();
        }

    }
}
