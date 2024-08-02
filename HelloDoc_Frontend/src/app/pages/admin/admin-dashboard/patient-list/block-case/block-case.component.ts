import { Component, Inject, Injector, Input } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AlphabetOnlyInputComponent } from '../../../../../shared/components/alphabet-only-input/alphabet-only-input.component';
import { ButtonComponent } from '../../../../../shared/components/button/button.component';
import { InputComponent } from '../../../../../shared/components/input/input.component';
import { TextareaComponent } from '../../../../../shared/components/textarea/textarea.component';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormSubmitDirective } from '../../../../../directives/form-submit.directive';
import { IBlockCase } from '../../../../../models/request/IBlockCase';
import { AdminService } from '../../../../../services/admin/admin.service';
import { IResponse } from '../../../../../models/response/IResponse';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from '../../../../../shared/services/notification.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-block-case',
  standalone: true,
  imports: [
    FormsModule,
    ButtonComponent,
    AlphabetOnlyInputComponent,
    ReactiveFormsModule,
    InputComponent,
    TextareaComponent,
    FormSubmitDirective,
  ],
  templateUrl: './block-case.component.html',
  styleUrl: './block-case.component.scss',
})
export class BlockCaseComponent {
  @Input() userName: string = '';
  @Input() userId!: number;

  constructor(
    public activeModal: NgbActiveModal,
    public adminService: AdminService,
    public notificationService: NotificationService,
    public router: Router
  ) {}

  blockCaseForm = new FormGroup({
    userName: new FormControl('', Validators.required),
    reasonForBlock: new FormControl('', Validators.required),
  });

  blockCase() {
    this.blockCaseForm.markAllAsTouched();
    if (this.blockCaseForm.valid) {
      const { reasonForBlock } = this.blockCaseForm.value;
      const payload: IBlockCase = {
        reasonForBlock,
        userId: this.userId,
      };
      console.log(payload);
      this.adminService.blockCase(payload).subscribe({
        next: (response: IResponse<null>) => {
          if (response.success) {
            this.notificationService.success(response.message);
          }
        },
        error: (error: HttpErrorResponse) => {
          this.notificationService.error(error.error.messages);
        },
      });
      this.activeModal.close(payload);
    }
  }

  close() {
    this.activeModal.dismiss();
  }
}
