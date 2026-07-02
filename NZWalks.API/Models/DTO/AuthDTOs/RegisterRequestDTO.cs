using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO.AuthDTOs
{
    public class RegisterRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string UserName{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
