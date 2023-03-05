namespace Authentication.Api.Models
{
    public class Token
    {
        public static readonly string DEFAULT_TOKEN_TYPE = "Beaker";

        public string Type { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public string AccessToken { get; set; } = null!;

        public Token(string type, string refreshToken, string accessToken)
        {
            Type = type;
            RefreshToken = refreshToken;
            AccessToken = accessToken;
        }
    }
}
