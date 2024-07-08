using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelloDoc_Entities.DTOs.Response
{
    public class VerifyOtpResponse
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