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
        public OrganizerType Type { get; set; } = OrganizerType.NORMAL;
        public string? Description { get; set; }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public OrganizerStatus Status { get; set; } = OrganizerStatus.ACTIVE;
        //newUserProfile;
        public int PartnerId { get; set; }
        public Partner Partner { get; set; } = null!;
        public ICollection<Organizer> Organizers { get; set; } = new HashSet<Organizer>();
        public ICollection<User> Users { get; set; } = new HashSet<User>();
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
