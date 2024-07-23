using HelloDoc_Entities.DataModels;
using HelloDoc_Entities.DTOs.Request;

namespace HelloDoc_Entities.ExtensionMethods
{
    public static class ProviderDetailsMappingProfile
    {
        public static ProviderDetails ToRegisterProviderDetailsRequest(this RegisterProviderRequest registerProviderRequest) => new()
        {
            MedicalLicense = registerProviderRequest.MedicalLicense,
            NpiNumber = registerProviderRequest.NpiNumber,
            BusinessName = registerProviderRequest.BusinessName,
            BusinessWebsite = registerProviderRequest.BusinessWebsite,
            Document = registerProviderRequest.Document,
            AdminNotes = registerProviderRequest.AdminNotes,
            ContractorAgreement = registerProviderRequest.ContractorAgreement,
            ContractorDocument = registerProviderRequest.ContractorDocument,
            BackgroundCheck = registerProviderRequest.BackgroundCheck,
            BackgroundCheckDocument = registerProviderRequest.BackgroundCheckDocument,
            HipaaCompliance = registerProviderRequest.HipaaCompliance,
            HipaaComplianceDocument = registerProviderRequest.HipaaComplianceDocument,
            NonDisclosureAgreement = registerProviderRequest.NonDisclosureAgreement,
            NonDisclosureDocument = registerProviderRequest.NonDisclosureDocument
        };

        public static void ToSetUserId(this ProviderDetails providerDetails, User user)
        {
            providerDetails.UserId = user.Id;
        }
    }
}