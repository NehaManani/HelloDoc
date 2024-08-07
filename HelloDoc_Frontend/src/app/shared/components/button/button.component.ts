import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss',
})
export class ButtonComponent {
  @Input() type: string = '';
  @Input() disabled: boolean = false;
  @Input() label: string = '';
  @Input() title: string = '';
  @Input() styles: any = '';
  @Input() className: string = '';
  @Input() iconClass: any = '';
  @Input() leftIcon: boolean = true;

  @Output() btnClick: EventEmitter<any> = new EventEmitter<any>();

  name: string = 'clicked';

  constructor() {}

  handleClick() {
    this.btnClick.emit(this.name);
  }
}
