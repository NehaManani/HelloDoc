import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-site-main-page',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule, RouterModule],
  templateUrl: './site-main-page.component.html',
  styleUrl: './site-main-page.component.scss',
})
export class SiteMainPageComponent {
  isDarkMode: boolean = false;
  toggleDarkMode() {
    this.isDarkMode = !this.isDarkMode;
  }
}
