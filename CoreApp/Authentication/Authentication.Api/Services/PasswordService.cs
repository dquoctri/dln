using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Api.Services
{
    public static class PasswordService
    {
        public static string GenerateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        public static string HashPassword(string password, string salt)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] hashBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                hashBytes = pbkdf2.GetBytes(32); // 32 bytes = 256 bits
            }
            return Convert.ToBase64String(hashBytes);
        }

        public static bool Compare(string password, string salt, string hash)
        {
            return HashPassword(password, salt) == hash;
        }
    }
}
