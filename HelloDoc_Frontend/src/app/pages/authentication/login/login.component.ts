import { Component } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ValidationPattern } from '../../../constants/validation/validation-pattern';
import { InputComponent } from '../../../shared/components/input/input.component';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { LoginService } from '../../../services/authentication/login.service';
import { ILogin } from '../../../models/request/Ilogin';
import { AuthService } from '../../../services/authentication/auth.service';
import { NotificationService } from '../../../shared/services/notification.service';
import { IResponse } from '../../../models/response/IResponse';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    InputComponent,
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    ButtonComponent,
    RouterModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  showPassword: boolean = false;
  isDarkMode: boolean = false;
  loginForm = new FormGroup({
    email: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.email),
      ])
    ),
    password: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.password),
      ])
    ),
    rememberMe: new FormControl(false),
  });

  constructor(
    private loginService: LoginService,
    private notificationService: NotificationService,
    private router: Router
  ) {}

  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
  }

  onLogin() {
    this.loginForm.markAllAsTouched();
    if (this.loginForm.valid)
      this.loginService.login(<ILogin>this.loginForm.value).subscribe({
        next: (response: IResponse<string>) => {
          if (response.success) {
            this.notificationService.success(response.message);
            this.router.navigate(['/verify-otp'], {
              queryParams: { email: this.loginForm.value.email },
            });
          }
        },
        error: (error: HttpErrorResponse) => {
          this.notificationService.error(error.error.messages);
        },
      });
  }
}
