import { Component } from '@angular/core';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { LoaderService } from '../../services/loader.service';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-loader',
  standalone: true,
  imports: [ProgressSpinnerModule, AsyncPipe, CommonModule],
  templateUrl: './loader.component.html',
  styleUrl: './loader.component.scss',
})
export class LoaderComponent {
  isLoading$ = this.loaderService.isLoading$;

  constructor(public loaderService: LoaderService) {}
}
