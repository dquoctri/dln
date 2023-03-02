using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Api.Services.Infrastructures
{
    public class PasswordService : IPasswordService
    {
        public string CreateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        public string CreateHash(string password, string salt)
        {
            // derive a 512-bit subkey (use HMACSHA512 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 512 / 8));
            return hashed;
        }

        public string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            byte[] hashBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
            {
                hashBytes = pbkdf2.GetBytes(32); // 32 bytes = 256 bits
            }

            return Convert.ToBase64String(hashBytes);
        }

        public byte[] GenerateSalt(int saltLength)
        {
            byte[] salt = new byte[saltLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public bool Validate(string password, string salt, string hash)
        {
            return CreateHash(password, salt) == hash;
        }
    }
}
