import { CommonModule } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import {
  ReactiveFormsModule,
  FormsModule,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FormSubmitDirective } from '../../../../../directives/form-submit.directive';
import { AlphabetOnlyInputComponent } from '../../../../../shared/components/alphabet-only-input/alphabet-only-input.component';
import { ButtonComponent } from '../../../../../shared/components/button/button.component';
import { InputComponent } from '../../../../../shared/components/input/input.component';
import { PhoneNumberInputComponent } from '../../../../../shared/components/phone-number-input/phone-number-input.component';
import { SelectComponent } from '../../../../../shared/components/select/select.component';
import { TextareaComponent } from '../../../../../shared/components/textarea/textarea.component';
import { HttpErrorResponse } from '@angular/common/http';
import { ValidationPattern } from '../../../../../constants/validation/validation-pattern';
import { IResponse } from '../../../../../models/response/IResponse';
import { NotificationService } from '../../../../../shared/services/notification.service';
import { DropdownItem } from '../../../../../shared/models/dropdown-item';
import { AdminService } from '../../../../../services/admin/admin.service';
import { IPatientDetailsResponse } from '../../../../../models/response/IPatientDetailsResponse';

@Component({
  selector: 'app-view-case',
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
  templateUrl: './view-case.component.html',
  styleUrl: './view-case.component.scss',
})
export class ViewCaseComponent {
  userId!: number;
  confirmationNumber: string = '';
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
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    phoneNumber: new FormControl(''),
    gender: new FormControl(''),
    city: new FormControl(''),
    zip: new FormControl(''),
    address: new FormControl(''),
    emergencyContactName: new FormControl(''),
    emergencyContactNumber: new FormControl(''),
    medicalHistory: new FormControl(''),
    allergies: new FormControl(''),
    currentMedications: new FormControl(''),
    bloodTypeId: new FormControl(''),
    document: new FormControl(),
    confirmationNumber: new FormControl(),
  });

  constructor(
    private adminService: AdminService,
    private notificationService: NotificationService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const userId = Number(params.get('userId'));
      this.getPatientDetails(userId);
    });
  }

  handlePictureFileChange(event: any) {
    const file = event.target.files?.[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.patientInfoForm.value.document = reader.result;
      this.uploadedDocument = reader.result;
    };
  }

  getPatientDetails(userId: number) {
    this.adminService.getPatientDetails(userId).subscribe({
      next: (response: IResponse<IPatientDetailsResponse>) => {
        if (response.success) {
          console.log(response.data);

          this.populateForm(response.data);
        }
      },
      error: (error: HttpErrorResponse) => {
        this.notificationService.error(error.error.messages);
      },
    });
  }

  populateForm(data: any) {
    this.confirmationNumber = data.confirmationNumber || ' ';
    this.uploadedDocument = data.document || '';
    this.patientInfoForm.patchValue({
      firstName: data.firstName,
      lastName: data.lastName,
      email: data.email,
      phoneNumber: data.phoneNumber,
      gender: data.gender,
      city: data.city,
      zip: data.zip,
      address: data.address,
      emergencyContactName: data.emergencyContactName,
      emergencyContactNumber: data.emergencyContactNumber,
      medicalHistory: data.medicalHistory,
      allergies: data.allergies,
      currentMedications: data.currentMedications,
      bloodTypeId: data.bloodTypeId,
      document: data.document,
    });
  }

  isImageDocument(document: any): boolean {
    return document.startsWith('data:image/');
  }
}
