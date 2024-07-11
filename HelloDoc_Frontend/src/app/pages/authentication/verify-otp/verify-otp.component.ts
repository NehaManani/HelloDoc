import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  ReactiveFormsModule,
  FormsModule,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { InputComponent } from '../../../shared/components/input/input.component';
import { ValidationPattern } from '../../../constants/validation/validation-pattern';
import { NotificationService } from '../../../shared/services/notification.service';
import { VerifyOtpService } from '../../../services/authentication/verify-otp.service';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../../../services/authentication/auth.service';
import { IResponse } from '../../../models/response/IResponse';

@Component({
  selector: 'app-verify-otp',
  standalone: true,
  imports: [
    InputComponent,
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    ButtonComponent,
    RouterModule,
  ],
  templateUrl: './verify-otp.component.html',
  styleUrl: './verify-otp.component.scss',
})
export class VerifyOtpComponent {
  email: string | null = null;
  from: string | null = null;
  showPassword: boolean = false;
  isDarkMode: boolean = false;
  verifyOtpForm = new FormGroup({
    otp: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(6),
        Validators.pattern(ValidationPattern.otp),
      ])
    ),
  });

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private verifyOtpService: VerifyOtpService,
    private authService: AuthService,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      this.email = params['email'];
      this.from = params['from'];
    });
  }

  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
  }

  onVerifyOtp(): void {
    this.verifyOtpForm.markAllAsTouched();
    if (this.verifyOtpForm.valid && this.email) {
      const otpValue = this.verifyOtpForm.get('otp')?.value;
      this.verifyOtpService.verifyOtp(this.email, otpValue).subscribe({
        next: (response: IResponse<string>) => {
          console.log(response);
          this.notificationService.success(response.message);
          const redirectUrl =
            this.from === 'forgot-password'
              ? '/reset-password'
              : '/create-patient';
              
          if (this.from === 'forgot-password') {
            this.router.navigate([redirectUrl], {
              queryParams: { email: this.email },
            });
          } else {
            this.authService.decodeToken(response.data);
            const userId = this.authService.getUserId() || '';
            console.log(userId);
            this.router.navigate([redirectUrl]);
          }
        },
        error: (error: HttpErrorResponse) => {
          this.notificationService.error(error.error.messages);
        },
      });
    }
  }

  onResendOtp(): void {
    this.verifyOtpService.resendOtp(this.email).subscribe({
      next: (response: IResponse<null>) => {
        console.log(response);
        this.notificationService.success(response.message);
      },
      error: (error: HttpErrorResponse) => {
        this.notificationService.error(error.error.messages);
      },
    });
  }
}
