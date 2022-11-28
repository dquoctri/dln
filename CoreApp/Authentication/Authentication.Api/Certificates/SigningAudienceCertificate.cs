using Authentication.Api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Common;
using System.Security.Cryptography;

namespace Authentication.Api.Certificates
{
    public class SigningAudienceCertificate : IDisposable
    {
        private readonly RSA rsa;
        private readonly SecretSettings _secretOptions;

        public SigningAudienceCertificate(IOptions<SecretSettings> secretOptions)
        {
            rsa = RSA.Create();
            _secretOptions = secretOptions.Value ?? throw new ArgumentNullException(nameof(secretOptions));
        }

        public SigningCredentials GetAudienceSigningKey()
        {
            // public key for decrypting
            string privateKey = File.ReadAllText(_secretOptions.AccessPrivateKeyPath);
            rsa.ImportFromPem(privateKey.ToCharArray());
            return new SigningCredentials(key: new RsaSecurityKey(rsa), algorithm: SecurityAlgorithms.RsaSha256);
        }

        public void Dispose()
        {
            rsa?.Dispose();
        }
    }
}
