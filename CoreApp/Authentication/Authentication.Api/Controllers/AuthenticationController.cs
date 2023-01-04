using Authentication.Api.DTOs;
using Authentication.Api.Models;
using Authentication.Api.Services;
using Authentication.Model;
using Authentication.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Common;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;
        private readonly IAccountRepository _accountRepository;

        public AuthenticationController(IUnitOfWork unitOfWork,
                                        IUserRepository userRepository,
                                        IOrganizerRepository organizerRepository,
                                        IAccountRepository accountRepository,
                                        ITokenService service,
                                        IPasswordService passwordService)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _tokenService = service;
            _passwordService = passwordService;
            _unitOfWork = unitOfWork;
            _organizerRepository = organizerRepository;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserCredential userCredential)
        {
            RefreshToken? token = _tokenService.CreateRefreshToken(userCredential);
            if (null == token) return Unauthorized();
            return Ok(token);
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] UserCredential userCredential)
        {
            string username = userCredential.Username.Trim();
            if (_accountRepository.IsExistedUsername(username))
            {
                return Conflict($"Username {username} is already in use.");
            }
            var organizer = _organizerRepository.GetOrganizerByName("System");
            string salt = _passwordService.CreateSalt();
            string hash = _passwordService.CreateHash(userCredential.Password, salt);
            var newAccount = new Account() { Username = username, PasswordHash = hash, Salt = salt, Organizer = organizer };
            _accountRepository.Insert(newAccount);
            await _unitOfWork.DeadlineAsync();
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            //todo
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("refresh")]
        [HttpPost]
        public IActionResult RefreshAccessToken()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //AccessToken? token = _tokenService.CreateAccessToken(userId);
            AccessToken? token = null;
            if (null == token) return Unauthorized();
            return Ok(token);
        }
    }
}
