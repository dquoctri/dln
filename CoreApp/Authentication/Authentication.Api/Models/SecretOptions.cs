namespace Authentication.Api.Models
{
    public class SecretOptions
    {
        public static readonly string CONFIG_KEY = "Secret";

        public string Issuer { get; set; } = "Deadl!ne";
        public string Audience { get; set; } = "Deadl!ne";
        public string PrivateKeyPath { get; set; } = string.Empty;
        public int AccessExpiryMinutes { get; set; }
        public string RefreshSecretKey { get; set; } = string.Empty;
        public int RefreshExpiryMinutes { get; set; }
    }
}
