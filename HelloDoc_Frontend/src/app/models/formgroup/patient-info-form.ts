export interface IPatientInfoForm {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  phoneNumber: string;
  gender: string;
  city?: string;
  zip?: string;
  address?: string;
  emergencyContactName?: string;
  emergencyContactNumber?: string;
  medicalHistory?: string;
  allergies?: string;
  currentMedications?: string;
  bloodTypeId?: number;
  document?: string;
}
