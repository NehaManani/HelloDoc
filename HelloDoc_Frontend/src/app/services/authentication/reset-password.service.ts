import { Injectable } from '@angular/core';
import { ApiCallConstant } from '../../constants/api-call/api';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IResponse } from '../../models/response/IResponse';
import { IResetPassword } from '../../models/request/IResetPassword';

@Injectable({
  providedIn: 'root',
})
export class ResetPasswordService {
  private resetPasswordApi = ApiCallConstant.RESET_PASSWORD_URL;

  constructor(private http: HttpClient) {}

  resetPassword(
    resetPasswordRequest: IResetPassword
  ): Observable<IResponse<null>> {
    return this.http.post<IResponse<null>>(
      this.resetPasswordApi,
      resetPasswordRequest
    );
  }
}
