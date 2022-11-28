using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.DTOs
{
    public class UserCredential
    {
        public UserCredential(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
