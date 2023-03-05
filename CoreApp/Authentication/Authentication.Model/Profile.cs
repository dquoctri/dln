using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace Authentication.Model
{
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Column("Permissions")]
        public ISet<Permission> Permissions { get; set; } = new HashSet<Permission>();

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }

        public int OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = null!;
        
    }
}
