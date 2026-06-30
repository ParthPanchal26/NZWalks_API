using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.RegionDTOs
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        public required string Code { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
