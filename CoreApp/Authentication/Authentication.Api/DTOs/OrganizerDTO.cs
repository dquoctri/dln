using Authentication.Model;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.DTOs
{
    public class OrganizerDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public int PartnerId { get; set; }

        public Organizer ToOrganizer()
        {
            return new Organizer()
            {
                Name = Name.Trim(),
                Description = Description,
                PartnerId = PartnerId,
            };
        }
    }
}
