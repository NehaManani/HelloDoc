import { Injectable } from '@angular/core';
import { ApiCallConstant } from '../../constants/api-call/api';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ILogin } from '../../models/Ilogin';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  loginApi = ApiCallConstant.LOGIN_URL;

  constructor(private http: HttpClient) {}

  login(loginCredentials: ILogin): Observable<ILogin> {
    return this.http.post<ILogin>(this.loginApi, loginCredentials, {
      withCredentials: true,
      headers: {
        credentials: 'include',
      },
    });
  }
}
