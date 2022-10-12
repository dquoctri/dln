using Authentication.Api.Models;
using Authentication.Api.Certificates;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Authentication.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly SigningAudienceCertificate signingAudienceCertificate;
        private readonly SecretOptions _secretOptions;

        public TokenService(IOptions<SecretOptions> secretOptions)
        {
            signingAudienceCertificate = new SigningAudienceCertificate(secretOptions);
            _secretOptions = secretOptions.Value;
        }

        public Token? CreateToken(UserCredential credential)
        {
            if (credential.Username != "dqtri" || credential.Password != "123456")
            {
                return null;
            }

            SecurityTokenDescriptor refreshTokenDescriptor = GetTokenDescriptor(credential);
            var refreshTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken refreshCecurityToken = refreshTokenHandler.CreateToken(refreshTokenDescriptor);
            string accessToken = refreshTokenHandler.WriteToken(refreshCecurityToken);

            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(credential.Username);
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = tokenHandler.WriteToken(securityToken);

            return new Token(accessToken, refreshToken);
        }

        public Token? CreateToken(string? userId)
        {
            if (userId == null) { return null; }
            if (!userId.Equals("dqtri")) { return null; }
            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(userId);
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);
            return new Token(accessToken);
        }

        private SecurityTokenDescriptor GetTokenDescriptor(UserCredential user)
        {
            //accesstoken
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Username) };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_secretOptions.RefreshExpiryMinutes),
                SigningCredentials = signingAudienceCertificate.GetAudienceSigningKey()
            };

            return tokenDescriptor;
        }

        private SecurityTokenDescriptor GetTokenDescriptor(string userId)
        {   //refeshtoken
            var Roles = new[] { "User", "Admin" };
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            claims.Add(new Claim(ClaimTypes.Name, userId));
            claims.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretOptions.RefreshSecretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _secretOptions.Issuer,
                Audience = _secretOptions.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_secretOptions.ExpiryMinutes),
                SigningCredentials = signingCredentials
            };

            return tokenDescriptor;
        }
    }
}
