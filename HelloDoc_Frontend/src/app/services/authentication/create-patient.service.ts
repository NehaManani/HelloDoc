import { Injectable } from '@angular/core';
import { ApiCallConstant } from '../../constants/api-call/api';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPatientInfoForm } from '../../models/formgroup/patient-info-form';
import { IResponse } from '../../models/response/IResponse';

@Injectable({
  providedIn: 'root',
})
export class CreatePatientService {
  submitRegisterPatientRequestApi =
    ApiCallConstant.SUBMIT_REGISTER_PATIENT_REQUEST;

  constructor(private http: HttpClient) {}

  SubmitRegisterPatientRequest(
    patientData: IPatientInfoForm
  ): Observable<IResponse<null>> {
    const formData = new FormData();
    for (const key in patientData) {
      if (
        patientData.hasOwnProperty(key) &&
        patientData[key as keyof IPatientInfoForm] !== undefined &&
        patientData[key as keyof IPatientInfoForm] !== null
      ) {
        formData.append(
          key,
          patientData[key as keyof IPatientInfoForm] as string | Blob
        );
      }
    }

    return this.http.post<IResponse<null>>(
      this.submitRegisterPatientRequestApi,
      formData
    );
  }
}
