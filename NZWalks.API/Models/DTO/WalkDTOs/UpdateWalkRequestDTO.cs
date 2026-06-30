using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.WalkDTOs
{
    public class UpdateWalkRequestDTO
    {
        [Required(ErrorMessage = "Walk name is required")]
        [MaxLength(100, ErrorMessage = "Walk name must be shorter")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Walk description is required")]
        [MaxLength(1000, ErrorMessage = "Description should be shorter than 1000 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Walk length is required")]
        public required double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }


        [Required(ErrorMessage = "Region is required for walk")]
        public required Guid RegionId { get; set; }

        [Required(ErrorMessage = "Difficulty is required for walk")]
        public required Guid DifficultyId { get; set; }
    }
}
