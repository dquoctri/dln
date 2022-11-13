using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "nvarchar(255)")]
        public ISet<UserRole> Roles { get; set; } = new HashSet<UserRole>();
    }
}
