import { Injectable } from '@angular/core';
import { ApiCallConstant } from '../../constants/api-call/api';
import { HttpClient } from '@angular/common/http';
import { IProviderInfoForm } from '../../models/formgroup/provider-info-form';
import { Observable } from 'rxjs';
import { IResponse } from '../../models/response/IResponse';

@Injectable({
  providedIn: 'root',
})
export class CreateProviderService {
  submitRegisterProviderRequestApi =
    ApiCallConstant.SUBMIT_REGISTER_PROVIDER_REQUEST;
  constructor(private http: HttpClient) {}

  // SubmitRegisterProviderRequest(
  //   providerData: FormData
  // ): Observable<IResponse<null>> {
  //   return this.http.post<IResponse<null>>(
  //     this.submitRegisterProviderRequestApi,
  //     providerData
  //   );
  // }

  SubmitRegisterProviderRequest(
    providerData: IProviderInfoForm
  ): Observable<IResponse<null>> {
    const formData = new FormData();
    for (const key in providerData) {
      if (
        providerData.hasOwnProperty(key) &&
        providerData[key as keyof IProviderInfoForm] !== undefined &&
        providerData[key as keyof IProviderInfoForm] !== null
      ) {
        formData.append(
          key,
          providerData[key as keyof IProviderInfoForm] as string | Blob
        );
      }
    }

    return this.http.post<IResponse<null>>(
      this.submitRegisterProviderRequestApi,
      formData
    );
  }
}
