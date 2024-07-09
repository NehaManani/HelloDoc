import { Routes } from '@angular/router';
import { CreatePatientComponent } from './pages/authentication/create-patient/create-patient.component';
import { ForgotPasswordComponent } from './pages/authentication/forgot-password/forgot-password.component';
import { LoginComponent } from './pages/authentication/login/login.component';
import { SiteMainPageComponent } from './pages/authentication/site-main-page/site-main-page.component';
import { UserSelectionComponent } from './pages/authentication/user-selection/user-selection.component';
import { VerifyOtpComponent } from './pages/authentication/verify-otp/verify-otp.component';

export const routes: Routes = [
  { path: '', component: SiteMainPageComponent },
  { path: 'user-selection', component: UserSelectionComponent },
  { path: 'login', component: LoginComponent },
  { path: 'verify-otp', component: VerifyOtpComponent },
  { path: 'create-patient', component: CreatePatientComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
];
