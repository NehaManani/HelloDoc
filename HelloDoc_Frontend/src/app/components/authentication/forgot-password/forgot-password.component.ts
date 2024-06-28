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
import { ForgotPasswordService } from '../../../services/authentication/forgot-password.service';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [
    InputComponent,
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    ButtonComponent,
    RouterModule,
  ],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent {
  isDarkMode: boolean = false;
  emailValidationMsg: string = ValidationMessageConstant.email;
  loginUrl: string = RoutingPathConstant.loginUrl;
  forgotPasswordForm = new FormGroup({
    email: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.email),
      ])
    ),
  });
  authService: any;
  notificationService: any;

  constructor(
    private titleService: Title,
    private router: Router,
    private forgotPasswordService: ForgotPasswordService
  ) {}

  ngOnInit() {
    this.router.events.subscribe(() => {
      this.titleService.setTitle('Reset Your Password');
    });
  }

  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
  }

  onSubmit() {
    this.forgotPasswordForm.markAllAsTouched();
    if (this.forgotPasswordForm.valid) {
      this.forgotPasswordService
        .forgotPassword(<string>this.forgotPasswordForm.value.email)
        .subscribe({
          next: (response: any) => {
            if (response.success) {
              console.log(response);
              this.notificationService.success(response.message);
            }
          },
          error: (error: any) => {
            console.log(error);
            this.notificationService.error(error.error.messages);
          },
        });
    }
  } 
}
