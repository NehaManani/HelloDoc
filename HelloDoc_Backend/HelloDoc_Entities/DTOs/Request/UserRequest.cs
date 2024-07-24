using System.ComponentModel.DataAnnotations;

namespace HelloDoc_Entities.DTOs.Request
{
    public class UserRequest
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public int? Gender { get; set; }

        public string? Role { get; set; }

        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? Address { get; set; }
    }
}