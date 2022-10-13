using Authentication.Api.Models;
using Authentication.Api.Certificates;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Net;

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

        public AccessToken? CreateAccessToken(string? userId)
        {
            if (userId != "dqtri")
            {
                return null;
            }

            SecurityTokenDescriptor refreshTokenDescriptor = GetAccessTokenDescriptor(userId);
            var refreshTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken refreshCecurityToken = refreshTokenHandler.CreateToken(refreshTokenDescriptor);
            string accessToken = refreshTokenHandler.WriteToken(refreshCecurityToken);
            return new AccessToken(AccessToken.DEFAULT_TOKEN_TYPE, accessToken);
        }

        public RefreshToken? CreateRefreshToken(UserCredential credential)
        {
            if (credential.Username != "dqtri" || credential.Password != "123456")
            {
                return null;
            }

            SecurityTokenDescriptor tokenDescriptor = GetRefreshTokenDescriptor(credential);
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = tokenHandler.WriteToken(securityToken);
            return new RefreshToken(RefreshToken.DEFAULT_TOKEN_TYPE, refreshToken);
        }

       
        private SecurityTokenDescriptor GetAccessTokenDescriptor(string userId)
        {
            var Roles = new[] { "User", "Admin" };
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            claims.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_secretOptions.AccessExpiryMinutes),
                SigningCredentials = signingAudienceCertificate.GetAudienceSigningKey()
            };

            return tokenDescriptor;
        }

        private SecurityTokenDescriptor GetRefreshTokenDescriptor(UserCredential user)
        {   
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Username), new Claim(ClaimTypes.Name, user.Username) };
            var refreshSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretOptions.RefreshSecretKey));
            var signingCredentials = new SigningCredentials(refreshSecurityKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _secretOptions.Issuer,
                Audience = _secretOptions.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_secretOptions.RefreshExpiryMinutes),
                SigningCredentials = signingCredentials
            };

            return tokenDescriptor;
        }
    }
}
