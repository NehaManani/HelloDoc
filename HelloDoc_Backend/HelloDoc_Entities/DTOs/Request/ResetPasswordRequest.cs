using System.ComponentModel.DataAnnotations;

namespace HelloDoc_Entities.DTOs.Request
{
    public class ResetPasswordRequest
    {
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{4,}$", ErrorMessage = "Invalid password format.")]
        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}