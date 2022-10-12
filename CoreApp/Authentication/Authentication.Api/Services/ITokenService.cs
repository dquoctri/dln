using Authentication.Api.Models;

namespace Authentication.Api.Services
{
    public interface ITokenService
    {
        public Token? CreateToken(UserCredential credential);
        public Token? CreateToken(string? userId);
    }
}
