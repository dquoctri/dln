using Authentication.Entity;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.DTOs
{
    public class PartnerDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Partner ToPartner()
        {
            return new Partner()
            {
                Name = Name.Trim(),
                Description = Description
            };
        }
    }
}
