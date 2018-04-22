using System.ComponentModel.DataAnnotations;

namespace kms.Dtos
{
    public class SigninDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
