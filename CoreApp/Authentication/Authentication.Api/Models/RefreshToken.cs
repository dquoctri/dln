namespace Authentication.Api.Models
{
    public class RefreshToken
    {
        public static readonly string DEFAULT_TOKEN_TYPE = "Beaker";

        public RefreshToken(string type, string token)
        {
            Type = type;
            Token = token;
        }

        public string Type { get; set; }
        public string Token { get; set; }
    }
}
