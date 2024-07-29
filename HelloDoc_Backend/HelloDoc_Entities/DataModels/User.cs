
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [MaxLength(16)]
        public byte Role { get; set; }

        [StringLength(10)]
        public string? OTP { get; set; }

        public DateTime? OtpExpiryTime { get; set; }

        public int? Gender { get; set; }

        [StringLength(13)]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(10)]
        public string? Zip { get; set; }

        [StringLength(512)]
        public string? Address { get; set; }

        public byte[]? Avatar { get; set; }

        public byte Status { get; set; }

        public string? ReasonForBlock { get; set; }

        [ForeignKey(nameof(Role))]
        public virtual UserRole UserRoles { get; set; } = null!;

        [ForeignKey(nameof(Status))]
        public virtual UserStatus UserStatuses { get; set; } = null!;

        [ForeignKey(nameof(Gender))]
        public virtual Gender Genders { get; set; } = null!;
    }
}