using System.ComponentModel.DataAnnotations;

namespace SciqusTraining.API.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }
        public string? FileDescripation { get; set; }
    }
}
