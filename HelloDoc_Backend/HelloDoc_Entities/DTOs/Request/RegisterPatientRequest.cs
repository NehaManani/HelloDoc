using HelloDoc_Entities.DataModels;

namespace HelloDoc_Entities.DTOs.Request
{
    public class RegisterPatientRequest : UserRequest
    {
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }
        public string? CurrentMedications { get; set; }
        public int? BloodTypeId { get; set; }
        public string? Document { get; set; }

        public User ReturnPatientRequestUser(RegisterPatientRequest registerPatientRequest)
        {
            return new User
            {
                FirstName = registerPatientRequest.FirstName,
                LastName = registerPatientRequest.LastName,
                Email = registerPatientRequest.Email,
                Password = registerPatientRequest.Password,
                PhoneNumber = registerPatientRequest.PhoneNumber,
                City = registerPatientRequest.City,
                Zip = registerPatientRequest.Zip,
                Address = registerPatientRequest.Address,
                Gender = registerPatientRequest.Gender,
            };
        }

        public PatientDetails ReturnPatientDetailsRequest(RegisterPatientRequest registerPatientRequest)
        {
            return new PatientDetails
            {
                EmergencyContactName = registerPatientRequest.EmergencyContactName,
                EmergencyContactNumber = registerPatientRequest.EmergencyContactNumber,
                MedicalHistory = registerPatientRequest.MedicalHistory,
                Allergies = registerPatientRequest.Allergies,
                CurrentMedications = registerPatientRequest.CurrentMedications,
                BloodTypeId = registerPatientRequest.BloodTypeId,
                Document = registerPatientRequest.Document,
            };
        }
    }
}