using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Model
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; } = null!;
        [Required(AllowEmptyStrings = false)]
        public string Hash { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Salt { get; set; } = null!;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }

        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = null!;
        public int ProfileId { get; set; }
        public Profile Profile { get; set; } = null!;
    }
}