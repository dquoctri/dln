using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Model
{
    [Index(nameof(Username), IsUnique = true)]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; } = null!;
        [Required(AllowEmptyStrings = false)]
        public string PasswordHash { get; set; } = null!;
        [Required(AllowEmptyStrings = false)]
        public string Salt { get; set; } = null!;
        public int OrganizerId { get; set; }
        public Organizer? Organizer { get; set; }
    }
}