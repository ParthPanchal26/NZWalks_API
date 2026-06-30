namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public required Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public required Guid RegionId { get; set; }
        public required Guid DifficultyId { get; set; }

        public required Difficulty Difficulty { get; set; }
        public required Region Region { get; set; }
    }
}
