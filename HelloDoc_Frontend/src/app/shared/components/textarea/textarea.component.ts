import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ControlErrorContainerDirective } from '../../../directives/control-error-container.directive';
import { ControlErrorsDirective } from '../../../directives/control-errors.directive';

@Component({
  selector: 'app-textarea',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    ControlErrorContainerDirective,
    ControlErrorsDirective,
  ],
  templateUrl: './textarea.component.html',
  styleUrl: './textarea.component.scss',
})
export class TextareaComponent {
  @Input() label!: string;
  @Input() labelClass: string = '';
  @Input() controlName: string = '';
  @Input() row: number = 3;
  @Input() column: number = 15;
  @Input() name: string = '';
  @Input() parentForm!: FormGroup;
  @Input() errorTitle!: string;
  @Input() className!: string;
  @Input() placeholder!: string;
  @Input() value: string = '';
  @Input() readonly: boolean = false;
  @Input({ required: false }) testId = '';
}
