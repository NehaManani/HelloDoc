import { Component } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router, RouterModule } from '@angular/router';
import { RoutingPathConstant } from '../../../constants/routing/routing-path';
import { ValidationMessageConstant } from '../../../constants/validation/validation-message';
import { ValidationPattern } from '../../../constants/validation/validation-pattern';
import { InputComponent } from '../../../shared/components/input/input.component';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { LoginService } from '../../../services/authentication/login.service';
import { ILogin } from '../../../models/Ilogin';
import { AuthService } from '../../../services/authentication/auth.service';
import { NotificationService } from '../../../shared/services/notification.service';

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
  emailValidationMsg: string = ValidationMessageConstant.email;
  passwordValidationMsg: string = ValidationMessageConstant.password;
  forgotPasswordUrl: string = RoutingPathConstant.forgotPasswordUrl;
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
    private authService: AuthService,
    private titleService: Title,
    private router: Router
  ) {}

  ngOnInit() {
    this.router.events.subscribe(() => {
      this.titleService.setTitle('Login To Your Account');
    });
  }

  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  rememberMeClick(checkbox: any) {
    this.loginForm.value.rememberMe = checkbox.target.checked;
  }

  onLogin() {
    this.loginForm.markAllAsTouched();
    if (this.loginForm.valid)
      this.loginService.login(<ILogin>this.loginForm.value).subscribe({
        next: (response: any) => {
          if (response.success) {
            console.log(response);

            this.authService.decodeToken(response.data);
            const userId = this.authService.getUserId() || '';
            console.log(userId);

            this.notificationService.success(response.message);
          }
        },
        error: (error) => {
          console.log(error);
          console.log(error.error.messages);

          this.notificationService.error(error.error.messages);
        },
      });
  }
}
