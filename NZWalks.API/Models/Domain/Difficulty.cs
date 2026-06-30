namespace NZWalks.API.Models.Domain
{
    public class Difficulty
    {
        public required Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
    }
}
