using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.Models.Partners
{
    public class PartnerRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
