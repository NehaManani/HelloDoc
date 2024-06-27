
using System.ComponentModel.DataAnnotations;
using HelloDoc_Entities.Abstract;

namespace HelloDoc_Entities.DataModels
{
    public class User : AuditableEntity<int>
    {
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(50)]
        public string Email { get; set; } = null!;

        [MaxLength(255)]
        public string Password { get; set; } = null!;

        [StringLength(10)]
        public string? OTP { get; set; }

        public DateTime? OtpExpiryTime { get; set; }

        [StringLength(13)]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(10)]
        public string? Zip { get; set; }

        [StringLength(512)]
        public string? Address { get; set; }

        public byte[]? Avatar { get; set; }
    }
}