import { Component, ElementRef, ViewChild } from '@angular/core';
import { ButtonComponent } from '../../../shared/components/button/button.component';
import { TextareaComponent } from '../../../shared/components/textarea/textarea.component';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IPatientInfoForm } from '../../../models/formgroup/patient-info-form';
import { AlphabetOnlyInputComponent } from '../../../shared/components/alphabet-only-input/alphabet-only-input.component';
import { ValidationPattern } from '../../../constants/validation/validation-pattern';
import { Router, RouterModule } from '@angular/router';
import { InputComponent } from '../../../shared/components/input/input.component';
import { PhoneNumberInputComponent } from '../../../shared/components/phone-number-input/phone-number-input.component';
import { SelectComponent } from '../../../shared/components/select/select.component';
import { DropdownItem } from '../../../shared/models/dropdown-item';
import { CreatePatientService } from '../../../services/authentication/create-patient.service';
import { NotificationService } from '../../../shared/services/notification.service';
import { IResponse } from '../../../models/response/IResponse';
import { HttpErrorResponse } from '@angular/common/http';
import { FormSubmitDirective } from '../../../directives/form-submit.directive';

@Component({
  selector: 'app-create-patient',
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
  templateUrl: './create-patient.component.html',
  styleUrl: './create-patient.component.scss',
})
export class CreatePatientComponent {
  uploadedDocument: string | ArrayBuffer | null = '';
  @ViewChild('photoInput') photoInput!: ElementRef;
  genderOptions: DropdownItem[] = [
    { value: 1, viewValue: 'Male' },
    { value: 2, viewValue: 'Female' },
  ];

  bloodTypeOptions: DropdownItem[] = [
    { value: 1, viewValue: 'A+' },
    { value: 2, viewValue: 'A-' },
    { value: 3, viewValue: 'B+' },
    { value: 4, viewValue: 'B-' },
    { value: 5, viewValue: 'AB+' },
    { value: 6, viewValue: 'AB-' },
    { value: 7, viewValue: 'O+' },
    { value: 8, viewValue: 'O-' },
  ];

  patientInfoForm = new FormGroup({
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
    emergencyContactName: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.names),
      ])
    ),
    emergencyContactNumber: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(ValidationPattern.phoneNumber),
      ])
    ),
    medicalHistory: new FormControl(''),
    allergies: new FormControl(''),
    currentMedications: new FormControl(
      '',
      Validators.compose([Validators.required])
    ),
    bloodTypeId: new FormControl(''),
    document: new FormControl(),
  });

  constructor(
    private createPatientService: CreatePatientService,
    private notificationService: NotificationService,
    private router: Router
  ) {}

  handlePictureFileChange(event: any) {
    const file = event.target.files?.[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      console.log(reader.result);
      this.patientInfoForm.value.document = reader.result;
      this.uploadedDocument = reader.result;
    };
  }

  onSubmit() {
    this.patientInfoForm.markAllAsTouched();
    if (this.patientInfoForm.valid) {
      this.patientInfoForm.value.document = this.uploadedDocument;
      this.createPatientService
        .SubmitRegisterPatientRequest(
          this.patientInfoForm.value as IPatientInfoForm
        )
        .subscribe({
          next: (response: IResponse<null>) => {
            if (response.success) {
              this.notificationService.success(response.message);
              this.router.navigate(['/login']);
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
