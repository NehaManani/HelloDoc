namespace HelloDoc_Entities.DTOs.Request
{
    public class RegisterPatientRequest : UserRequest
    {
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }
        public string CurrentMedications { get; set; }
        public int? BloodTypeId { get; set; }
        public string? Document { get; set; }
        public string? ConfirmationNumber { get; set; }

    }
}