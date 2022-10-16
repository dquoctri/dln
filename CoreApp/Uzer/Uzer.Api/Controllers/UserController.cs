using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Uzer.Api.Services;
using Uzer.Repository;

namespace Uzer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            var users2 = await _userRepository.GetAllAsync();
            _logger.Log(LogLevel.Information, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            return Ok(users2);
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
