using Authentication.Service.Models;

namespace Authentication.Service
{
    public interface IAuthenticationService
    {
        public string Login(LoginRequest request);
    }
}
