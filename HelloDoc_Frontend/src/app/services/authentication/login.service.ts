import { Injectable } from '@angular/core';
import { ApiCallConstant } from '../../constants/api-call/api';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IResponse } from '../../models/response/IResponse';
import { ILogin } from '../../models/request/ILogin';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  loginApi = ApiCallConstant.LOGIN_URL;

  constructor(private http: HttpClient) {}

  login(loginCredentials: ILogin): Observable<IResponse<string>> {
    return this.http.post<IResponse<string>>(this.loginApi, loginCredentials, {
      withCredentials: true,
      headers: {
        credentials: 'include',
      },
    });
  }
}
