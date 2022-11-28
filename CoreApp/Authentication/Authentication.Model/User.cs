using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        public bool EmailVerified { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrganizerId { get; set; }
        public Organizer? Organizer { get; set; }
        public int ProfileId { get; set; }
        public Profile? Profile { get; set; }
    }
}
