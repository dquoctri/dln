namespace Authentication.Api.Models
{
    public class SecretSettings
    {
        public static readonly string CONFIG_SECTION_KEY = "Secret";

        public string Issuer { get; set; } = "Deadl!ne";
        public string Audience { get; set; } = "Deadl!ne";
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiryMinutes { get; set; }
        public string AccessPrivateKeyPath { get; set; } = string.Empty;
        public int AccessExpiryMinutes { get; set; }
    }
}
