using Authentication.Api.Models;

namespace Authentication.Api.Services
{
    public interface ITokenService
    {
        public AccessToken? CreateAccessToken(string? userId);
        public RefreshToken? CreateRefreshToken(UserCredential credential);
    }


}
