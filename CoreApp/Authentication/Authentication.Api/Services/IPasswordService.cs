namespace Authentication.Api.Services
{
    public interface IPasswordService
    {
        public string CreateHash(string password, string salt);
        public string CreateSalt();
        public bool Validate(string password, string salt, string hash);
    }
}
