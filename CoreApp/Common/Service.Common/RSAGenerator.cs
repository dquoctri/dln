using System.Security.Cryptography;

namespace Service.Common
{
    public static class RSAGenerator
    {
        public static string ExportPrivateKeyInPEMFormat(RSA rsa)
        {
            var privateKeyBytes = rsa.ExportRSAPrivateKey();
            var builder = new System.Text.StringBuilder();
            builder.AppendLine("-----BEGIN RSA PRIVATE KEY-----");
            builder.AppendLine(Convert.ToBase64String(privateKeyBytes, Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END RSA PRIVATE KEY-----");
            return builder.ToString();
        }

        public static string ExportPublicKeyInPEMFormat(RSA rsa)
        {
            var publicKeyBytes = rsa.ExportRSAPublicKey();
            var builder = new System.Text.StringBuilder();
            builder.AppendLine("-----BEGIN PUBLIC KEY-----");
            builder.AppendLine(Convert.ToBase64String(publicKeyBytes, Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END PUBLIC KEY-----");
            return builder.ToString();
        }
    }
}