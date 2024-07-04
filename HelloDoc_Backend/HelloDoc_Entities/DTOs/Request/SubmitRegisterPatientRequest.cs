using Microsoft.AspNetCore.Http;

namespace HelloDoc_Entities.DTOs.Request
{
    public class SubmitRegisterPatientRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? City { get; set; }
        public string? Zip { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }
        public string? CurrentMedications { get; set; }
        public int? BloodTypeId { get; set; }
        public IFormFile Document { get; set; }
    }
}