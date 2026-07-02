using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Image
    {
        public required Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public required string FileName { get; set; }
        public string? FileDescription { get; set; }
        public required string FileExtension { get; set; }
        public required long FileSizeInBytes { get; set; }
        public required string FilePath { get; set; }
    }
}
