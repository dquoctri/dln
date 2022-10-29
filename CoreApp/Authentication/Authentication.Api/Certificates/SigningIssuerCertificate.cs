using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Authentication.Api.Certificates
{
    public class SigningIssuerCertificate : IDisposable
    {
        private readonly RSA rsa;

        public SigningIssuerCertificate()
        {
            rsa = RSA.Create();
        }

        public RsaSecurityKey GetIssuerSigningKey()
        {
            string cartPemKey = File.ReadAllText("./public.pem");
            rsa.ImportFromPem(cartPemKey.ToCharArray());
            return new RsaSecurityKey(rsa);
        }
        public void Dispose()
        {
            rsa?.Dispose();
        }
    }
}
