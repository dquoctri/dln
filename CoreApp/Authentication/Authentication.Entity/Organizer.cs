using Authentication.Entity.Converters;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Entity
{
    public class Organizer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;
        [Column(TypeName = "nvarchar(24)")]
        public OrganizerType Type { get; set; } = OrganizerType.NORMAL;
        public string? Description { get; set; }
        [DefaultValue("getutcdate()")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; } 
        public DateTime? ModifiedDate { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public OrganizerStatus Status { get; set; } = OrganizerStatus.ACTIVE;
        //newUserProfile;
        public int PartnerId { get; set; }
        public Partner Partner { get; set; } = null!;
        public ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }

    public enum OrganizerStatus
    {
        [Description("ACTIVE")] ACTIVE,
        [Description("LOCKED")] LOCKED,
        [Description("DELETED")] DELETED
    }

    public enum OrganizerType
    {
        [Description("SYSTEM")] SYSTEM,
        [Description("PARTNER")] PARTNER,
        [Description("NORMAL")] NORMAL
    }
}
