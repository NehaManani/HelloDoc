using System.ComponentModel.DataAnnotations;

namespace HelloDoc_Entities.DTOs.Request
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}