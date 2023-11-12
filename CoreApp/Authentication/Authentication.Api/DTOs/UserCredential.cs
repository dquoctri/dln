using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.DTOs
{
    public class UserCredential
    {
        public UserCredential(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
