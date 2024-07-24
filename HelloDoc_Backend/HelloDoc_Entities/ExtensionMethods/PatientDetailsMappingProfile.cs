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

        public static void ToConfirmationNumber(this PatientDetails patientDetails, string ConfirmationNumber)
        {
            patientDetails.ConfirmationNumber = ConfirmationNumber;
        }

        public static RegisterPatientRequest ToGetPatientDetails(this User user, PatientDetails? patientDetails)
        {
            return new RegisterPatientRequest
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Genders?.Id,
                City = user.City,
                Zip = user.Zip,
                Address = user.Address,
                EmergencyContactName = patientDetails?.EmergencyContactName,
                EmergencyContactNumber = patientDetails?.EmergencyContactNumber,
                MedicalHistory = patientDetails?.MedicalHistory,
                Allergies = patientDetails?.Allergies,
                CurrentMedications = patientDetails?.CurrentMedications,
                BloodTypeId = patientDetails?.BloodTypeId,
                Document = patientDetails?.Document,
                ConfirmationNumber = patientDetails?.ConfirmationNumber
            };
        }
    }
}