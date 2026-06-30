using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.RegionDTOs
{
    public class UpdateRegionRequestDTO
    {
        [Required(ErrorMessage = "Region code is required")]
        [MinLength(3, ErrorMessage = "Code need to be 3 character long")]
        [MaxLength(3, ErrorMessage = "Code need to be 3 character long")]
        public required string Code { get; set; }

        [Required(ErrorMessage = "Region name is required")]
        [MaxLength(100, ErrorMessage = "Region name must be shorter")]
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
