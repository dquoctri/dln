using Authentication.Model;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.DTOs
{
    public class AccountDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; } = null!;

        public int OrganizerId { get; set; }

        public User ToAccount()
        {
            return new User() {
                Username = Username,
                OrganizerId = OrganizerId
            };
        }
    }
}
