import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/authentication/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const authToken = this.authService.getJwtToken();
    if (authToken === null || authToken === '') return next.handle(request);
    const authRequest = request.clone({
      setHeaders: {
        Authorization: `${authToken}`,
        'Access-Control-Allow-Origin': 'http://localhost:4200',
        Accept: 'application/pdf',
      },
      withCredentials: true,
    });
    return next.handle(authRequest);
  }
}
