import { Injectable } from '@angular/core';
import { ApiCallConstant } from '../../constants/api-call/api';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CreatePatientService {
  submitRegisterPatientRequestApi =
    ApiCallConstant.SUBMIT_REGISTER_PATIENT_REQUEST;

  constructor(private http: HttpClient) {}

  SubmitRegisterPatientRequest(patientData: any): Observable<any> {
    console.log(patientData);

    const formData = new FormData();
    for (const key in patientData) {
      if (patientData.hasOwnProperty(key)) {
        formData.append(key, patientData[key]);
      }
    }

    return this.http.post<any>(this.submitRegisterPatientRequestApi, formData);
  }
}
