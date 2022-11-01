using Authentication.Entity;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.Models.Partners
{
    public class PartnerRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public Partner ToPartner()
        {
            return new Partner()
            { 
                Name = Name.Trim(),
                Description = String.IsNullOrWhiteSpace(Description) ? null : Description.Trim()
            };
        }
    }
}
