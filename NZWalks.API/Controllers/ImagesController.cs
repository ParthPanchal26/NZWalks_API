using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.ImageDTOs;
using NZWalks.API.Repositories.ImageRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        #region post
        [HttpPost("upload")]
        public async Task<ActionResult> UploadImage([FromForm] ImageUploadRequestDTO req)
        {
            ValidateFileUpload(req);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var imageDomainModel = new Image()
            {
                Id = Guid.NewGuid(),
                File = req.File,
                FileName = req.FileName,
                FileDescription = req.FileDescription,
                FileExtension = Path.GetExtension(req.File.FileName),
                FilePath = req.File.FileName,
                FileSizeInBytes = req.File.Length
            };

            await _imageRepository.Upload(imageDomainModel);

            return Ok(imageDomainModel);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }


        #endregion

    }
}
