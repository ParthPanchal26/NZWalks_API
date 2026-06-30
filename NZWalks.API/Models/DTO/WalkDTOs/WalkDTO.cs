namespace NZWalks.API.Models.DTO.WalkDTOs
{
    public class WalkDTO
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public required Guid RegionId { get; set; }
        public required Guid DifficultyId { get; set; }
    }
}
