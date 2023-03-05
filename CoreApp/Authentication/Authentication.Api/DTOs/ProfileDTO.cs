using Authentication.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Api.DTOs
{
    public class ProfileDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        public ISet<Permission> Roles { get; set; } = new HashSet<Permission>();

        public Profile ToProfile()
        {
            return new Profile()
            {
                Name = Name.Trim(),
                Description = Description,
                Permissions = Roles
            };
        }
    }
}
