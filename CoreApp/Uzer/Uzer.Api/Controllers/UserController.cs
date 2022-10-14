using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Uzer.Repository;

namespace Uzer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUnitOfWork _unitOfWork;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("validate")]
        [HttpGet]
        public async Task<IActionResult> ValidateAsync()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var a = await _unitOfWork.Users.GetByIdAsync(long.Parse(userId));
            _logger.Log(LogLevel.Information, "Validate JwtBearerDefaults.AuthenticationScheme ");
            return Ok(a);
        }
    }
}
