using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SciqusTraining.API.Models.Domains;
using SciqusTraining.API.Models.DTO;
using SciqusTraining.API.Repositories;

namespace SciqusTraining.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                // convert dto to domain module 
                var imageDomainModule = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescripation
                };
                // user repository to uplode img
                await imageRepository.Upload(imageDomainModule);

                return Ok(imageDomainModule);
            }
            return BadRequest(ModelState);  
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", "png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10Mb, please uplode a smaller size file");
            }
        }
    }
}
