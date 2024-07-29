import { Routes } from '@angular/router';
import { CreatePatientComponent } from './pages/authentication/create-patient/create-patient.component';
import { ForgotPasswordComponent } from './pages/authentication/forgot-password/forgot-password.component';
import { LoginComponent } from './pages/authentication/login/login.component';
import { SiteMainPageComponent } from './pages/authentication/site-main-page/site-main-page.component';
import { UserSelectionComponent } from './pages/authentication/user-selection/user-selection.component';
import { VerifyOtpComponent } from './pages/authentication/verify-otp/verify-otp.component';
import { CreateProviderComponent } from './pages/authentication/create-provider/create-provider.component';
import { ResetPasswordComponent } from './pages/authentication/reset-password/reset-password.component';
import { AdminDashboardComponent } from './pages/admin/admin-dashboard/admin-dashboard.component';
import { PatientListComponent } from './pages/admin/admin-dashboard/patient-list/patient-list.component';
import { ViewCaseComponent } from './pages/admin/admin-dashboard/patient-list/view-case/view-case.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', component: SiteMainPageComponent },
  { path: 'user-selection', component: UserSelectionComponent },
  { path: 'login', component: LoginComponent },
  { path: 'verify-otp', component: VerifyOtpComponent },
  { path: 'create-patient', component: CreatePatientComponent },
  { path: 'create-provider', component: CreateProviderComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  {
    path: 'admin-dashboard',
    component: AdminDashboardComponent,
    children: [
      {
        path: '',
        redirectTo: '/admin-dashboard/patient-list',
        pathMatch: 'full',
      },
      { path: 'patient-list', component: PatientListComponent },
      { path: 'view-case/:userId', component: ViewCaseComponent },
    ],
    canActivate: [AuthGuard],
  },
];
