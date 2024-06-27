import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { StorageHelperConstant } from '../../shared/storage-helper/storage-helper';
import { StorageHelperService } from './storage-helper.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  hasAccess() {
    throw new Error('Method not implemented.');
  }
  constructor(
    private router: Router,
    private jwtService: JwtHelperService,
    private http: HttpClient,
    private storageHelper: StorageHelperService
  ) {}

  decodeToken(token: string) {
    const decodedToken = this.jwtService.decodeToken(token);
    const userId = decodedToken['UserId'];
    const userName =
      decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
      ];
    const userEmail =
      decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
      ];
    this.storageHelper.setAsLocal(StorageHelperConstant.authToken, token);
    this.storageHelper.setAsLocal(StorageHelperConstant.userId, userId);
    this.storageHelper.setAsLocal(StorageHelperConstant.userName, userName);
    this.storageHelper.setAsLocal(StorageHelperConstant.email, userEmail);
  }

  isJwtTokenExpire() {
    return this.jwtService.isTokenExpired(this.getJwtToken());
  }

  isAuthenticate() {
    if (this.getJwtToken() === null || this.getJwtToken() === '') {
      return false;
    } else {
      return true;
    }
  }

  getJwtToken() {
    return this.storageHelper.getFromLocal(StorageHelperConstant.authToken);
  }

  getUserId() {
    return this.storageHelper.getFromLocal(StorageHelperConstant.userId);
  }

  getUserName() {
    return this.storageHelper.getFromLocal(StorageHelperConstant.userName);
  }

  getUserEmail() {
    return this.storageHelper.getFromLocal(StorageHelperConstant.email);
  }

  logOut(): void {
    this.storageHelper.removeFromLocal(StorageHelperConstant.authToken);
    this.storageHelper.removeFromLocal(StorageHelperConstant.userId);
    this.storageHelper.removeFromLocal(StorageHelperConstant.userName);
    this.storageHelper.removeFromLocal(StorageHelperConstant.email);
    this.router.navigate(['/']);
  }
}
