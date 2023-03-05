using Authentication.Api.DTOs;
using Authentication.Api.Models;

namespace Authentication.Api.Services
{
    public interface ITokenService
    {
        public Token? CreateToken(UserCredential credential);
        public AccessToken? CreateAccessToken(Guid? userId);
    }
}
