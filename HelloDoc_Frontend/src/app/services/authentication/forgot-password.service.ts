import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiCallConstant } from '../../constants/api-call/api';
import { Observable } from 'rxjs';
import { IResponse } from '../../models/response/IResponse';

@Injectable({
  providedIn: 'root',
})
export class ForgotPasswordService {
  forgotPasswordApi = ApiCallConstant.FORGOT_PASSWORD_URL;

  constructor(private http: HttpClient) {}

  forgotPassword(email: string) {
    const body = { email: email };
    return this.http.post<IResponse<null>>(this.forgotPasswordApi, body);
  }
}
