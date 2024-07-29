import { Injectable } from '@angular/core';
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from '../services/authentication/auth.service';
import { NotificationService } from '../shared/services/notification.service';
import { NotificationMessageConstant } from '../constants/validation/notification-message';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthService,
    private messageService: NotificationService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const isAuthenticated = this.authService.isAuthenticate();

    if (!isAuthenticated) {
      this.messageService.error(NotificationMessageConstant.loginFirst);
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }
}
