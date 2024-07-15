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
import { ValidationPattern } from '../../../constants/validation/validation-pattern';
import { InputComponent } from '../../../shared/components/input/input.component';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { ForgotPasswordService } from '../../../services/authentication/forgot-password.service';
import { NotificationService } from '../../../shared/services/notification.service';
import { HttpErrorResponse } from '@angular/common/http';
import { IResponse } from '../../../models/response/IResponse';
import { SystemConstants } from '../../../constants/system-constants/system-constants';
import * as CryptoJS from 'crypto-js';

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

  forgotPasswordForm = new FormGroup({
    email: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.email),
      ])
    ),
  });

  constructor(
    private titleService: Title,
    private router: Router,
    private forgotPasswordService: ForgotPasswordService,
    private notificationService: NotificationService
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
          next: (response: IResponse<null>) => {
            if (response.success) {
              this.notificationService.success(response.message);
              this.router.navigate(['/verify-otp'], {
                queryParams: {
                  email: CryptoJS.AES.encrypt(
                    this.forgotPasswordForm.value.email ?? '',
                    SystemConstants.EncryptionKey
                  ),
                  from: CryptoJS.AES.encrypt(
                    'forgot-password',
                    SystemConstants.EncryptionKey
                  ),
                },
              });
            }
          },
          error: (error: HttpErrorResponse) => {
            this.notificationService.error(error.error.messages);
          },
        });
    }
  }
}
