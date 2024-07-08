import { Routes } from '@angular/router';
import { LoginComponent } from './components/authentication/login/login.component';
import { ForgotPasswordComponent } from './components/authentication/forgot-password/forgot-password.component';
import { CreatePatientComponent } from './components/authentication/create-patient/create-patient.component';
import { UserSelectionComponent } from './components/authentication/user-selection/user-selection.component';
import { SiteMainPageComponent } from './components/authentication/site-main-page/site-main-page.component';

export const routes: Routes = [
  { path: '', component: SiteMainPageComponent },
  { path: 'user-selection', component: UserSelectionComponent },
  { path: 'login', component: LoginComponent },
  { path: 'create-patient', component: CreatePatientComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
];
