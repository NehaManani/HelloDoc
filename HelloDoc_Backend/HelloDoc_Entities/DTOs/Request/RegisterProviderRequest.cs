using System.ComponentModel.DataAnnotations;
using HelloDoc_Entities.DataModels;

namespace HelloDoc_Entities.DTOs.Request
{
    public class RegisterProviderRequest : UserRequest
    {
        [Required]
        public string MedicalLicense { get; set; } = null!;

        [Required]
        public string NpiNumber { get; set; } = null!;

        public string? BusinessName { get; set; } = null!;

        public string? BusinessWebsite { get; set; }

        public string? Document { get; set; }

        public string? AdminNotes { get; set; }

        public bool ContractorAgreement { get; set; }

        public string? ContractorDocument { get; set; }

        public bool BackgroundCheck { get; set; }

        public string? BackgroundCheckDocument { get; set; }

        public bool HipaaCompliance { get; set; }

        public string? HipaaComplianceDocument { get; set; }

        public bool NonDisclosureAgreement { get; set; }

        public string? NonDisclosureDocument { get; set; }

    }
}