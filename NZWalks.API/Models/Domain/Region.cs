namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        public required Guid Id { get; set; } = Guid.NewGuid();
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
