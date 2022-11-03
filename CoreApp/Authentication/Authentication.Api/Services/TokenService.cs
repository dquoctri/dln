using Authentication.Api.Models;
using Authentication.Api.Certificates;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Authentication.Entity;
using Authentication.Repository;
using Repository.Common;

namespace Authentication.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly SigningAudienceCertificate signingAudienceCertificate;
        private readonly SecretOptions _secretOptions;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;

        public TokenService(IOptions<SecretOptions> secretOptions, IUnitOfWork unitOfWork, IAccountRepository accountRepository)
        {
            signingAudienceCertificate = new SigningAudienceCertificate(secretOptions);
            _secretOptions = secretOptions.Value;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }

        public AccessToken? CreateAccessToken(string? userId)
        {
            if (userId != "dqtri")
            {
                return null;
            }
            Account? account = _accountRepository.GetAccountByUsername(userId);
            if (account == null)
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
            Account? account = _accountRepository.GetAccountByUsername(credential.Username);

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
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Name, user.Username) };
            claims.Add(new Claim("organization", user.Username));

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
