using System.ComponentModel.DataAnnotations;

namespace HelloDoc_Entities.DTOs.Request
{
    public class VerifyOtpRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$",
        ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required]
        public string Otp { get; set; } = null!;
    }
}