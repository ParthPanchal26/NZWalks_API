using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.WalkDTOs
{
    public class CreateWalkRequestDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        [Required]
        public required Guid RegionId { get; set; }
        [Required]
        public required Guid DifficultyId { get; set; }
    }
}
