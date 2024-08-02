import { Component, Input } from '@angular/core';
import { ButtonComponent } from '../button/button.component';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../services/authentication/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [ButtonComponent, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  role: string = '';
  userName: string = this.authService.getUserName();
  navTabs: { label: string; link: string }[] = [];

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.role = this.authService.getUserRole();
    this.setNavTabs();
  }
  setNavTabs() {
    if (this.role === '1') {
      this.navTabs = [
        { label: 'Dashboard', link: '/admin-dashboard' },
        { label: 'Provider Dashboard', link: '/admin-dashboard/provider-list' },
        { label: 'My Profile', link: '/my-profile' },
        { label: 'Providers', link: '/providers' },
        { label: 'Partners', link: '/partners' },
        { label: 'Access', link: '/access' },
        { label: 'Records', link: '/records' },
      ];
    } else if (this.role === '2') {
      this.navTabs = [
        { label: 'Dashboard', link: '/dashboard' },
        { label: 'My Profile', link: '/my-profile' },
        { label: 'Records', link: '/records' },
      ];
    }
  }

  onLogout() {
    this.authService.logOut();
  }
}
