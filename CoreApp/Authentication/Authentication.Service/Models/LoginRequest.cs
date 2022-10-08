using System.ComponentModel.DataAnnotations;

namespace Authentication.Service.Models
{
    public class LoginRequest
    {
        public LoginRequest(string username, string password)
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
