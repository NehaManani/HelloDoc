export interface IProviderInfoForm {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  phoneNumber: string;
  gender: string;
  city?: string;
  zip?: string;
  address?: string;
  medicalLicense: string;
  npiNumber: string;
  businessName: string;
  businessWebsite?: string;
  document?: string;
  adminNotes?: string;
  contractorAgreement: boolean;
  contractorDocument?: string;
  backgroundCheck: boolean;
  backgroundCheckDocument?: string;
  hipaaCompliance: boolean;
  hipaaComplianceDocument?: string;
  nonDisclosureAgreement: boolean;
  nonDisclosureDocument?: string;
}
