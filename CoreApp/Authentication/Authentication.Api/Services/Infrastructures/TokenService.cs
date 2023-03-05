using Authentication.Api.Certificates;
using Authentication.Api.DTOs;
using Authentication.Api.Models;
using Authentication.Model;
using Authentication.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Common;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Authentication.Api.Services.Infrastructures
{
    public class TokenService : ITokenService
    {
        private readonly SigningAudienceCertificate signingAudienceCertificate;
        private readonly SecretSettings _secretOptions;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _accountRepository;

        public TokenService(IOptions<SecretSettings> secretOptions, IUnitOfWork unitOfWork, IUserRepository accountRepository)
        {
            signingAudienceCertificate = new SigningAudienceCertificate(secretOptions);
            _secretOptions = secretOptions.Value;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }

        public AccessToken? CreateAccessToken(Guid? userId)
        {
            User? account = _accountRepository.GetByID(userId);
            if (account == null)
            {
                return null;
            }
            SecurityTokenDescriptor accessTokenDescriptor = GetAccessTokenDescriptor(userId);
            var accessTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken accessCecurityToken = accessTokenHandler.CreateToken(accessTokenDescriptor);
            string accessToken = accessTokenHandler.WriteToken(accessCecurityToken);
            return new AccessToken(AccessToken.DEFAULT_TOKEN_TYPE, accessToken);
        }

        public Token? CreateToken(UserCredential credential)
        {
            User? account = _accountRepository.GetAccountByUsername(credential.Username);

            SecurityTokenDescriptor tokenDescriptor = GetRefreshTokenDescriptor(credential);
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = tokenHandler.WriteToken(securityToken);
            return new Token(Token.DEFAULT_TOKEN_TYPE, refreshToken, refreshToken);
        }


        private SecurityTokenDescriptor GetRefreshTokenDescriptor(UserCredential user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.Username) };
            claims.Add(new Claim("organization", user.Username));

            var refreshSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretOptions.SecretKey));
            var signingCredentials = new SigningCredentials(refreshSecurityKey, SecurityAlgorithms.HmacSha512Signature);
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

        private SecurityTokenDescriptor GetAccessTokenDescriptor(Guid? userId)
        {
            var Roles = new[] { "User", "Admin" };
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId?.ToString() ?? "") };
            claims.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_secretOptions.AccessExpiryMinutes),
                SigningCredentials = signingAudienceCertificate.GetAudienceSigningKey()
            };

            return tokenDescriptor;
        }
    }
}
