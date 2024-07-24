export interface IPatientDetailsResponse {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  gender: number;
  city?: string;
  zip?: string;
  address?: string;
  emergencyContactName?: string;
  emergencyContactNumber?: string;
  medicalHistory?: string;
  allergies?: string;
  currentMedications: string;
  bloodTypeId?: number;
  document?: string;
  confirmationNumber?: string;
}
