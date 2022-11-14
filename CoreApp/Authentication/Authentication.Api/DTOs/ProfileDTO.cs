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
        public ISet<UserRole> Roles { get; set; } = new HashSet<UserRole>();

        public Profile ToProfile()
        {
            return new Profile()
            {
                Name = Name.Trim(),
                Description = Description,
                Roles = Roles
            };
        }
    }
}
