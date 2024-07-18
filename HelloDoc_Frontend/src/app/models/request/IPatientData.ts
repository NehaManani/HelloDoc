export interface IPatientData {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  password: string | null;
  gender: number;
  role: string;
  city: string | null;
  zip: string | null;
  address: string | null;
}
