using Authentication.Api.DTOs;
using Authentication.Api.Models;
using Authentication.Api.Services;
using Authentication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Common;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthController(IUnitOfWork unitOfWork,
                                        IUserRepository userRepository,
                                        IOrganizerRepository organizerRepository,
                                        ITokenService service,
                                        IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenService = service;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _organizerRepository = organizerRepository;
            _configuration = configuration;
        }
        //This API endpoint is used to log in a user and obtain an access token and a refresh token.
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserCredential userCredential)
        {
            Token? token = _tokenService.CreateToken(userCredential);
            if (null == token) return Unauthorized();
            return Ok(token);
        }

        //This API endpoint is used to obtain a new access token using a refresh token.
        [Authorize]
        [Route("refresh")]
        [HttpPost]
        public IActionResult Refresh()
        {
            var currentUser = HttpContext.User;
            var userName = HttpContext.User.Identity?.Name;
            return Ok(_tokenService.CreateAccessToken(new Guid()));
        }


        //This API endpoint is used to obtain a new access token using a refresh token.
        [Authorize]
        [Route("logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            //add refest token to blacklist
            return Ok("");
        }


        //This API endpoint is used to create a new user account.
        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public IActionResult Register()
        {

            return Ok("");
        }

        //This API endpoint allows a user to change their password.
        [Authorize]
        [Route("change-password")]
        [HttpPost]
        public IActionResult ChangePassword()
        {

            return Ok("");
        }

        //This API endpoint is used to initiate the password reset process.
        [AllowAnonymous]
        [Route("forgot-password")]
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {

            return Ok("");
        }

        //This API endpoint is used to reset a user's password.
        [AllowAnonymous]
        [Route("reset-password")]
        [HttpPost]
        public IActionResult ResetPassword()
        {

            return Ok("");
        }


        //[HttpPost("login2")]
        //public async Task<IActionResult> Login([FromBody] LoginRequest request)
        //{
        //    // TODO: Validate the username and password against your user database
        //    if (request.Username != "myusername" || request.Password != "mypassword")
        //    {
        //        return Unauthorized();
        //    }

        //    // Create the JWT token
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, request.Username)
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpirationMinutes"])),
        //        //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    // Return the JWT token as a string
        //    return Ok(new { Token = tokenHandler.WriteToken(token) });
        //}




        //[AllowAnonymous]
        //[Route("register")]
        //[HttpPost]
        //public async Task<IActionResult> RegisterAsync([FromBody] UserCredential userCredential)
        //{
        //    string username = userCredential.Username.Trim();
        //    if (_userRepository.IsExistedUsername(username))
        //    {
        //        return Conflict($"Username {username} is already in use.");
        //    }
        //    var organizer = _organizerRepository.GetOrganizerByName("System");
        //    string salt = _passwordService.CreateSalt();
        //    string hash = _passwordService.CreateHash(userCredential.Password, salt);
        //    var newAccount = new User() { Username = username, Hash = hash, Salt = salt, Organizer = organizer };
        //    _userRepository.Insert(newAccount);
        //    await _unitOfWork.DeadlineAsync();
        //    return Ok();
        //}

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Route("logout")]
        //[HttpPost]
        //public IActionResult Logout()
        //{
        //    //todo
        //    return Ok();
        //}

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Route("refresh")]
        //[HttpPost]
        //public IActionResult RefreshAccessToken()
        //{
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    //AccessToken? token = _tokenService.CreateAccessToken(userId);
        //    AccessToken? token = null;
        //    if (null == token) return Unauthorized();
        //    return Ok(token);
        //}
    }
}
