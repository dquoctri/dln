using Authentication.Api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Authentication.Api.Certificates
{
    public class SigningAudienceCertificate : IDisposable
    {
        private readonly RSA rsa;
        private readonly IOptions<SecretSettings> _secretOptions;

        public SigningAudienceCertificate(IOptions<SecretSettings> secretOptions)
        {
            rsa = RSA.Create();
            _secretOptions = secretOptions;
        }

        public SigningCredentials GetAudienceSigningKey()
        {
            // public key for decrypting
            string privateXmlKey = File.ReadAllText(_secretOptions.Value.PrivateKeyPath);
            rsa.ImportFromPem(privateXmlKey.ToCharArray());
            return new SigningCredentials(key: new RsaSecurityKey(rsa), algorithm: SecurityAlgorithms.RsaSha256);
        }

        public void Dispose()
        {
            rsa?.Dispose();
        }
    }
}
