using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelloDoc_Entities.Abstract;

namespace HelloDoc_Entities.DataModels
{
    public class PatientDetails : AuditableEntity<int>
    {
        public int UserId { get; set; }

        [MaxLength(255)]
        public string? EmergencyContactName { get; set; }

        [MaxLength(15)]
        public string? EmergencyContactNumber { get; set; }

        [MaxLength(500)]
        public string? MedicalHistory { get; set; }

        [MaxLength(500)]
        public string? Allergies { get; set; }

        [MaxLength(500)]
        public string? CurrentMedications { get; set; }

        public int? BloodTypeId { get; set; }

        public byte[]? Document { get; set; }

        [ForeignKey(nameof(UserId))]
        public User Users { get; set; } = null!;

        [ForeignKey(nameof(BloodTypeId))]
        public BloodType BloodType { get; set; } = null!;
    }
}