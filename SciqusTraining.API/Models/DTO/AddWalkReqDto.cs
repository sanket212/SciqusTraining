using System.ComponentModel.DataAnnotations;

namespace SciqusTraining.API.Models.DTO
{
    public class AddWalkReqDto
    {
        [Required]
        [MaxLength(100,ErrorMessage ="Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be a maximum of 1000 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0,50, ErrorMessage ="Length of walk should be between 0 and 50 km")]
        public double LengthInKm { get; set; }

        [Required]
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }

}
