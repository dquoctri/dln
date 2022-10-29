using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Entity
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
        public long OrganisationId { get; set; }
        public Organizer Organizer { get; set; } = null!;
    }
}
