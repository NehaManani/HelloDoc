using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HelloDoc_Entities.Abstract;

namespace HelloDoc_Entities.DataModels
{
    public class ProviderDetails : AuditableEntity<int>
    {
        public int UserId { get; set; }

        [StringLength(20)]
        public string MedicalLicense { get; set; } = null!;

        [StringLength(20)]
        public string NpiNumber { get; set; } = null!;

        [StringLength(100)]
        public string? BusinessName { get; set; } = null!;

        public string? BusinessWebsite { get; set; }

        public byte[]? Document { get; set; }

        public string? AdminNotes { get; set; }

        public bool ContractorAgreement { get; set; }

        public byte[]? ContractorDocument { get; set; }

        public bool BackgroundCheck { get; set; }

        public byte[]? BackgroundCheckDocument { get; set; }

        public bool HipaaCompliance { get; set; }

        public byte[]? HipaaComplianceDocument { get; set; }

        public bool NonDisclosureAgreement { get; set; }

        public byte[]? NonDisclosureDocument { get; set; }

        [ForeignKey(nameof(UserId))]
        public User Users { get; set; } = null!;
    }
}