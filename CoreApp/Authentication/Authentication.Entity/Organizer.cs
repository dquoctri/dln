using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Entity
{
    [Index(nameof(PartnerId), nameof(Name), IsUnique = true)]
    public class Organizer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;

        [Column(TypeName = "nvarchar(24)")]
        public OrganizerType Type { get; set; } = OrganizerType.NORMAL;
        public string? Description { get; set; }

        [DefaultValue("CURRENT_TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public OrganizerStatus Status { get; set; } = OrganizerStatus.ACTIVE;

        public int? ProfileId { get; set; }
        public Profile? Profile { get; set; }
        public int PartnerId { get; set; }
        public Partner Partner { get; set; } = null!;
        public ICollection<User> Users { get; set; } = new List<User>();
    }

    public enum OrganizerStatus
    {
        [Description("Active")] ACTIVE,
        [Description("Deleted")] DELETED,
        [Description("Staging")] STAGING,
    }

    public enum OrganizerType
    {
        [Description("System")] SYSTEM,
        [Description("Partner")] PARTNER,
        [Description("Normal")] NORMAL
    }

    public enum PackageStatus : short
    {
        New,
        PendingValidation,
        CheckedOut,
        Approved,
        Rejected,
        PendingRender,
        Rendered,
        RenderFailed,
        PendingPrint,
        Printed,
        PrintFailed,
        Cancelled,
        PendingDownload,
        Downloaded,
        DownloadError,
        SystemUpdated,
    }
}
