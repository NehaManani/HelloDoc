import { Component } from '@angular/core';
import { IPatientData } from '../../../../models/request/IPatientData';
import { NgClass, DecimalPipe, AsyncPipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  NgbHighlight,
  NgbTypeaheadModule,
  NgbPaginationModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { AdminService } from '../../../../services/admin/admin.service';
import { IPaginatedRequest } from '../../../../models/request/IPaginatedRequest';
import { IResponse } from '../../../../models/response/IResponse';
import { IPaginatedResponse } from '../../../../models/response/IPaginatedResponse';
import { debounceTime, Subject } from 'rxjs';
import { NotificationService } from '../../../../shared/services/notification.service';

@Component({
  selector: 'app-patient-list',
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
    NgbDropdownModule,
  ],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.scss',
})
export class PatientListComponent {
  activeCard: string = 'All';
  datasource: IPatientData[] = [];
  page = 1;
  pageSize = 7;
  collectionSize!: number;
  searchQuery!: string;
  searchSubject = new Subject<string>();
  sortColumn: string = 'FirstName';
  sortOrder: string = 'ascending';

  constructor(
    private adminService: AdminService,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    this.searchSubject.pipe(debounceTime(1000)).subscribe((searchQuery) => {
      this.search(searchQuery);
    });
    this.getPatientListRequest();
  }

  onKeyup(searchQuery: string) {
    this.searchSubject.next(searchQuery);
  }

  search(searchQuery: string) {
    this.searchQuery = searchQuery;
    this.getPatientListRequest();
  }

  setActiveCard(card: string) {
    this.activeCard = card;
    this.page = 1;
    this.getPatientListRequest();
  }

  changeSorting(column: string) {
    if (this.sortColumn === column) {
      this.sortOrder =
        this.sortOrder === 'ascending' ? 'descending' : 'ascending';
    } else {
      this.sortColumn = column;
      this.sortOrder = 'ascending';
    }
    this.getPatientListRequest();
  }

  getPatientListRequest() {
    const request: IPaginatedRequest = {
      pageIndex: this.page,
      pageSize: this.pageSize,
      sortOrder: this.sortOrder,
      sortColumn: this.sortColumn,
      searchQuery: this.searchQuery,
    };

    this.adminService.getPatientList(request).subscribe({
      next: (response: IResponse<IPaginatedResponse<IPatientData[]>>) => {
        if (response.success) {
          this.datasource = response.data.records;
          this.collectionSize = response.data.totalRecords;
        }
      },
      error: (error) => {
        this.notificationService.error(error.error.messages);
      },
    });
  }
}
