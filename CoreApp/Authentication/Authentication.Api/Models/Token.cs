namespace Authentication.Api.Models
{
    public class Token
    {
        public Token(string accessToken)
        {
            AccessToken = accessToken;
        }

        public Token(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
