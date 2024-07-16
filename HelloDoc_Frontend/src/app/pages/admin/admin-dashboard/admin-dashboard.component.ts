import { Component, PipeTransform } from '@angular/core';
import { HeaderComponent } from '../../../shared/components/header/header.component';
import { AsyncPipe, DecimalPipe, NgClass } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  NgbHighlight,
  NgbPaginationModule,
  NgbTypeaheadModule,
} from '@ng-bootstrap/ng-bootstrap';
import { IPatientData } from '../../../models/request/IPatientData';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [
    HeaderComponent,
    NgClass,
    FormsModule,
    ReactiveFormsModule,
    DecimalPipe,
    AsyncPipe,
    ReactiveFormsModule,
    NgbHighlight,
    NgbTypeaheadModule,
    NgbPaginationModule,
  ],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.scss',
})
export class AdminDashboardComponent {
  activeCard: string = 'All';
  datasource: IPatientData[] = [
    {
      name: 'test, test',
      dob: 'Jun 16, 2023',
      requester: 'Patient test, test',
      requestedDate: 'Nov 20, 2023',
      phone: '+1 (202) 466-1237',
      address: 'Room Location: 101',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'testP, testP',
      dob: 'Oct 20, 2023',
      requester: 'Patient testP, testP',
      requestedDate: 'Oct 20, 2023',
      phone: '+91 078945 64156',
      address: '123 baltimore, maryland 20880',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'name, patient',
      dob: 'Oct 18, 2023',
      requester: 'Concierge Tatvasoft',
      requestedDate: 'Oct 18, 2023',
      phone: '+1 (202) 714-5789',
      address: '123 baltimore, maryland 20880',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'test, test',
      dob: 'Oct 16, 2023',
      requester: 'Patient test, test',
      requestedDate: 'Oct 16, 2023',
      phone: '+91 082006 99201',
      address: '123 baltimore, maryland 20880',
      notes: 'Admin transferred to Dr. AGCA on 1/10/2023 at 10:07:08 AM',
      chatWith: 'Provider',
    },
    {
      name: 'test, test',
      dob: 'Jun 16, 2023',
      requester: 'Patient test, test',
      requestedDate: 'Nov 20, 2023',
      phone: '+1 (202) 466-1237',
      address: 'Room Location: 101',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'testP, testP',
      dob: 'Oct 20, 2023',
      requester: 'Patient testP, testP',
      requestedDate: 'Oct 20, 2023',
      phone: '+91 078945 64156',
      address: '123 baltimore, maryland 20880',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'name, patient',
      dob: 'Oct 18, 2023',
      requester: 'Concierge Tatvasoft',
      requestedDate: 'Oct 18, 2023',
      phone: '+1 (202) 714-5789',
      address: '123 baltimore, maryland 20880',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'test, test',
      dob: 'Oct 16, 2023',
      requester: 'Patient test, test',
      requestedDate: 'Oct 16, 2023',
      phone: '+91 082006 99201',
      address: '123 baltimore, maryland 20880',
      notes: 'Admin transferred to Dr. AGCA on 1/10/2023 at 10:07:08 AM',
      chatWith: 'Provider',
    },
    {
      name: 'test, test',
      dob: 'Jun 16, 2023',
      requester: 'Patient test, test',
      requestedDate: 'Nov 20, 2023',
      phone: '+1 (202) 466-1237',
      address: 'Room Location: 101',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'testP, testP',
      dob: 'Oct 20, 2023',
      requester: 'Patient testP, testP',
      requestedDate: 'Oct 20, 2023',
      phone: '+91 078945 64156',
      address: '123 baltimore, maryland 20880',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'name, patient',
      dob: 'Oct 18, 2023',
      requester: 'Concierge Tatvasoft',
      requestedDate: 'Oct 18, 2023',
      phone: '+1 (202) 714-5789',
      address: '123 baltimore, maryland 20880',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'test, test',
      dob: 'Oct 16, 2023',
      requester: 'Patient test, test',
      requestedDate: 'Oct 16, 2023',
      phone: '+91 082006 99201',
      address: '123 baltimore, maryland 20880',
      notes: 'Admin transferred to Dr. AGCA on 1/10/2023 at 10:07:08 AM',
      chatWith: 'Provider',
    },
    {
      name: 'test, test',
      dob: 'Jun 16, 2023',
      requester: 'Patient test, test',
      requestedDate: 'Nov 20, 2023',
      phone: '+1 (202) 466-1237',
      address: 'Room Location: 101',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'testP, testP',
      dob: 'Oct 20, 2023',
      requester: 'Patient testP, testP',
      requestedDate: 'Oct 20, 2023',
      phone: '+91 078945 64156',
      address: '123 baltimore, maryland 20880',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'name, patient',
      dob: 'Oct 18, 2023',
      requester: 'Concierge Tatvasoft',
      requestedDate: 'Oct 18, 2023',
      phone: '+1 (202) 714-5789',
      address: '123 baltimore, maryland 20880',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'test, test',
      dob: 'Oct 16, 2023',
      requester: 'Patient test, test',
      requestedDate: 'Oct 16, 2023',
      phone: '+91 082006 99201',
      address: '123 baltimore, maryland 20880',
      notes: 'Admin transferred to Dr. AGCA on 1/10/2023 at 10:07:08 AM',
      chatWith: 'Provider',
    },
    {
      name: 'test, test',
      dob: 'Jun 16, 2023',
      requester: 'Patient test, test',
      requestedDate: 'Nov 20, 2023',
      phone: '+1 (202) 466-1237',
      address: 'Room Location: 101',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'testP, testP',
      dob: 'Oct 20, 2023',
      requester: 'Patient testP, testP',
      requestedDate: 'Oct 20, 2023',
      phone: '+91 078945 64156',
      address: '123 baltimore, maryland 20880',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'name, patient',
      dob: 'Oct 18, 2023',
      requester: 'Concierge Tatvasoft',
      requestedDate: 'Oct 18, 2023',
      phone: '+1 (202) 714-5789',
      address: '123 baltimore, maryland 20880',
      notes: '',
      chatWith: 'Provider',
    },
    {
      name: 'test, test',
      dob: 'Oct 16, 2023',
      requester: 'Patient test, test',
      requestedDate: 'Oct 16, 2023',
      phone: '+91 082006 99201',
      address: '123 baltimore, maryland 20880',
      notes: 'Admin transferred to Dr. AGCA on 1/10/2023 at 10:07:08 AM',
      chatWith: 'Provider',
    },
  ];
  page = 1;
  pageSize = 4;
  collectionSize = this.datasource.length;

  constructor() {
    this.refreshDataSource();
  }

  setActiveCard(card: string) {
    this.activeCard = card;
  }

  refreshDataSource() {
    this.datasource = this.datasource
      .map((item, i) => ({
        id: i + 1,
        ...item,
      }))
      .slice(
        (this.page - 1) * this.pageSize,
        (this.page - 1) * this.pageSize + this.pageSize
      );
  }
}
