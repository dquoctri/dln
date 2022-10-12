using Authentication.Service.Models;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Authentication.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        public string Login(LoginRequest request)
        {
            if (request.Username != "dqtri" || request.Password != "123456")
            {
                return StringValues.Empty;
            }

            //var issuer = builder.Configuration["Jwt:Issuer"];
            //var audience = builder.Configuration["Jwt:Audience"];
            //var key = Encoding.ASCII.GetBytes
            //(builder.Configuration["Jwt:Key"]);
            var issuer = "https://dqtri.com/";
            var audience = "https://dqtri.com/";
            var key = Encoding.ASCII.GetBytes("TFa9VhLuncw#58&I76Cp6&v1#56!M3OF");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, request.Username),
                    new Claim(JwtRegisteredClaimNames.Email, request.Username),
                    new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
