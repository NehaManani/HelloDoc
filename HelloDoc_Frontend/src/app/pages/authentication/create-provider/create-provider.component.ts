import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, ViewChild } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { ValidationPattern } from '../../../constants/validation/validation-pattern';
import { IResponse } from '../../../models/response/IResponse';
import { NotificationService } from '../../../shared/services/notification.service';
import { DropdownItem } from '../../../shared/models/dropdown-item';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AlphabetOnlyInputComponent } from '../../../shared/components/alphabet-only-input/alphabet-only-input.component';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { InputComponent } from '../../../shared/components/input/input.component';
import { PhoneNumberInputComponent } from '../../../shared/components/phone-number-input/phone-number-input.component';
import { SelectComponent } from '../../../shared/components/select/select.component';
import { TextareaComponent } from '../../../shared/components/textarea/textarea.component';
import { FormSubmitDirective } from '../../../directives/form-submit.directive';
import { IProviderInfoForm } from '../../../models/formgroup/provider-info-form';
import { CreateProviderService } from '../../../services/authentication/create-provider.service';

@Component({
  selector: 'app-create-provider',
  standalone: true,
  imports: [
    ButtonComponent,
    TextareaComponent,
    ReactiveFormsModule,
    CommonModule,
    FormsModule,
    RouterModule,
    AlphabetOnlyInputComponent,
    InputComponent,
    PhoneNumberInputComponent,
    SelectComponent,
    FormSubmitDirective,
  ],
  templateUrl: './create-provider.component.html',
  styleUrl: './create-provider.component.scss',
})
export class CreateProviderComponent {
  uploadedDocument: string | ArrayBuffer | null = '';
  fileNames: { [key: string]: string } = {};
  @ViewChild('photoInput') photoInput!: ElementRef;
  @ViewChild('contractorAgreementFile') contractorAgreementFile!: ElementRef;
  @ViewChild('backgroundCheckFile') backgroundCheckFile!: ElementRef;
  @ViewChild('hipaaComplianceFile') hipaaComplianceFile!: ElementRef;

  @ViewChild('nonDisclosureAgreementFile')
  nonDisclosureAgreementFile!: ElementRef;

  genderOptions: DropdownItem[] = [
    { value: 1, viewValue: 'Male' },
    { value: 2, viewValue: 'Female' },
  ];

  providerInfoForm = new FormGroup({
    firstName: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.pattern(ValidationPattern.names),
      ])
    ),
    lastName: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.pattern(ValidationPattern.names),
      ])
    ),
    email: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.pattern(ValidationPattern.email),
      ])
    ),
    password: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(6),
        Validators.pattern(ValidationPattern.password),
      ])
    ),
    phoneNumber: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.phoneNumber),
      ])
    ),
    gender: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.minLength(2)]),
    zip: new FormControl('', [
      Validators.maxLength(6),
      Validators.pattern(/^[0-9]{6}$/),
    ]),
    address: new FormControl(''),
    medicalLicense: new FormControl('', [Validators.required]),
    npiNumber: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(10),
        Validators.pattern(ValidationPattern.npiNumber),
      ])
    ),
    businessName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.pattern(ValidationPattern.names),
    ]),
    businessWebsite: new FormControl('', [
      Validators.pattern(ValidationPattern.link),
    ]),
    document: new FormControl(),
    adminNotes: new FormControl(''),
    contractorAgreement: new FormControl(false),
    contractorDocument: new FormControl(),
    backgroundCheck: new FormControl(false),
    backgroundCheckDocument: new FormControl(),
    hipaaCompliance: new FormControl(false),
    hipaaComplianceDocument: new FormControl(),
    nonDisclosureAgreement: new FormControl(false),
    nonDisclosureDocument: new FormControl(),
  });

  constructor(
    private createProviderService: CreateProviderService,
    private notificationService: NotificationService
  ) {}

  handleFileChange(event: any, formControlName: string, fileNameKey: string) {
    const file = event.target.files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        console.log(reader.result);
        this.providerInfoForm.patchValue({ [formControlName]: reader.result });
        this.fileNames[fileNameKey] = file.name;
      };
    }
  }

  triggerFileInputClick(id: string) {
    const element: HTMLElement = document.getElementById(id) as HTMLElement;
    element.click();
  }

  handleContractorAgreementFileChange(event: any) {
    this.handleFileChange(
      event,
      'contractorDocument',
      'contractorDocumentFileName'
    );
  }

  handleBackgroundCheckFileChange(event: any) {
    this.handleFileChange(
      event,
      'backgroundCheckDocument',
      'backgroundCheckDocumentFileName'
    );
  }

  handleHipaaComplianceFileChange(event: any) {
    this.handleFileChange(
      event,
      'hipaaComplianceDocument',
      'hipaaComplianceDocumentFileName'
    );
  }

  handleNonDisclosureAgreementFileChange(event: any) {
    this.handleFileChange(
      event,
      'nonDisclosureDocument',
      'nonDisclosureDocumentFileName'
    );
  }

  onSubmit() {
    this.providerInfoForm.markAllAsTouched();
    if (this.providerInfoForm.valid) {
      this.providerInfoForm.value.document = this.uploadedDocument;
      this.createProviderService
        .SubmitRegisterProviderRequest(
          this.providerInfoForm.value as IProviderInfoForm
        )
        .subscribe({
          next: (response: IResponse<null>) => {
            if (response.success) {
              console.log(response);
              this.notificationService.success(response.message);
            }
          },
          error: (error: HttpErrorResponse) => {
            console.log(error);
            this.notificationService.error(error.error.messages);
          },
        });
    }
  }
}
