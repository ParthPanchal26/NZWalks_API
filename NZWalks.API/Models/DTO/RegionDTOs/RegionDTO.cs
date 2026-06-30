namespace NZWalks.API.Models.DTO.RegionDTOs
{
    public class RegionDTO
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
