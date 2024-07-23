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
import { IStatusCount } from '../../../../models/response/IStatusCounts';

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
  pageIndex = 1;
  pageSize = 7;
  collectionSize!: number;
  searchQuery!: string;
  status: string = '';
  searchSubject = new Subject<string>();
  sortColumn: string = 'FirstName';
  sortOrder: string = 'ascending';
  statusCounts: IStatusCount = {
    new: 0,
    pending: 0,
    active: 0,
    conclude: 0,
    close: 0,
    unpaid: 0,
  };
  constructor(
    private adminService: AdminService,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    this.searchSubject.pipe(debounceTime(1000)).subscribe((searchQuery) => {
      this.search(searchQuery);
    });
    this.getPatientListRequest();
    this.getStatusCount();
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
    this.status = card === 'All' ? '' : card;
    this.pageIndex = 1;
    this.getPatientListRequest();
  }

  onStatusChange(event: Event) {
    const selectedStatus = (event.target as HTMLSelectElement).value;
    this.setActiveCard(selectedStatus);
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
    console.log(this.status);

    const request: IPaginatedRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sortOrder: this.sortOrder,
      sortColumn: this.sortColumn,
      searchQuery: this.searchQuery,
      status: this.status,
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

  getStatusCount() {
    this.adminService.statusCountList().subscribe({
      next: (response: IResponse<IStatusCount>) => {
        if (response.success) {
          this.statusCounts = response.data;
        }
      },
      error: (error) => {
        this.notificationService.error(error.error.messages);
      },
    });
  }

  getActiveCardCount(): number {
    if (this.activeCard === 'All') {
      return this.collectionSize > 0 ? this.collectionSize : 0;
    }

    switch (this.activeCard) {
      case 'New':
        return this.statusCounts.new;
      case 'Pending':
        return this.statusCounts.pending;
      case 'Active':
        return this.statusCounts.active;
      case 'Conclude':
        return this.statusCounts.conclude;
      case 'Close':
        return this.statusCounts.close;
      case 'Unpaid':
        return this.statusCounts.unpaid;
      default:
        return 0;
    }
  }
}
