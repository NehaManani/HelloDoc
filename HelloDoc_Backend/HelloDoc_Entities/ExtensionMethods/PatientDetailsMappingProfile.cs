using HelloDoc_Entities.DataModels;
using HelloDoc_Entities.DTOs.Request;

namespace HelloDoc_Entities.ExtensionMethods
{
    public static class PatientDetailsMappingProfile
    {
        public static PatientDetails ToRegisterPatientDetailsRequest(this RegisterPatientRequest registerPatientRequest) => new()
        {
            EmergencyContactName = registerPatientRequest.EmergencyContactName,
            EmergencyContactNumber = registerPatientRequest.EmergencyContactNumber,
            MedicalHistory = registerPatientRequest.MedicalHistory,
            Allergies = registerPatientRequest.Allergies,
            CurrentMedications = registerPatientRequest.CurrentMedications,
            BloodTypeId = registerPatientRequest.BloodTypeId,
            Document = registerPatientRequest.Document,
        };

        public static void ToSetUserId(this PatientDetails patientDetails, User user)
        {
            patientDetails.UserId = user.Id;
        }
    }
}