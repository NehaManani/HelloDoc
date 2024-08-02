import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiCallConstant } from '../../constants/api-call/api';
import { IResponse } from '../../models/response/IResponse';

@Injectable({
  providedIn: 'root',
})
export class VerifyOtpService {
  verifyOtpApi = ApiCallConstant.VERIFY_OTP_URL;
  resendOtpApi = ApiCallConstant.RESEND_OTP_URL;

  constructor(private http: HttpClient) {}

  verifyOtp(
    email: string,
    otp: string | null | undefined
  ): Observable<IResponse<string>> {
    const body = { email, otp };
    return this.http.post<IResponse<string>>(`${this.verifyOtpApi}`, body, {
      withCredentials: true,
      headers: {
        credentials: 'include',
      },
    });
  }

  resendOtp(email: string | null): Observable<IResponse<null>> {
    const body = { email: email };
    return this.http.post<IResponse<null>>(`${this.resendOtpApi}`, body);
  }
}
