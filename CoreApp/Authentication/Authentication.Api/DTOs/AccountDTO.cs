using Authentication.Model;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.DTOs
{
    public class AccountDTO
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public int OrganizerId { get; set; }

        public User ToAccount()
        {
            return new User() {
                Email = Email,
                OrganizerId = OrganizerId
            };
        }
    }
}
