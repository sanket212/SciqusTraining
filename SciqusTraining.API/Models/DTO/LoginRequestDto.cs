using System.ComponentModel.DataAnnotations;

namespace SciqusTraining.API.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
    }
}
