import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ValidationPattern } from '../../../constants/validation/validation-pattern';
import { IResponse } from '../../../models/response/IResponse';
import { NotificationService } from '../../../shared/services/notification.service';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { InputComponent } from '../../../shared/components/input/input.component';
import { passwordMatchValidator } from '../../../common/password-match';
import { ResetPasswordService } from '../../../services/authentication/reset-password.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [
    InputComponent,
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    ButtonComponent,
    RouterModule,
  ],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
})
export class ResetPasswordComponent {
  email: string | null = null;
  showPassword: boolean = false;
  isDarkMode: boolean = false;
  resetPasswordForm = new FormGroup({
    password: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.password),
      ])
    ),
    confirmPassword: new FormControl(
      '',
      [Validators.required],
      [passwordMatchValidator()]
    ),
  });

  constructor(
    private resetPasswordService: ResetPasswordService,
    private notificationService: NotificationService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      this.email = params['email'];
    });
  }

  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
  }

  onResetPassword(): void {
    this.resetPasswordForm.markAllAsTouched();
    if (this.resetPasswordForm.valid) {
      const resetPasswordRequest = {
        email: this.email,
        password: this.resetPasswordForm.value.password,
      };

      this.resetPasswordService.resetPassword(resetPasswordRequest).subscribe({
        next: (response: IResponse<null>) => {
          if (response.success) {
            this.notificationService.success(response.message);
            this.router.navigate(['/login']);
          }
        },
        error: (error: HttpErrorResponse) => {
          this.notificationService.error(error.error.messages);
        },
      });
    }
  }
}
