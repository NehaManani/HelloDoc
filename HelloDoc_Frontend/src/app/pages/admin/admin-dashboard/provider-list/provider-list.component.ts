import { NgClass, DecimalPipe, AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  NgbHighlight,
  NgbTypeaheadModule,
  NgbPaginationModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { Router } from '@angular/router';
import { Subject, debounceTime } from 'rxjs';
import { IPaginatedRequest } from '../../../../models/request/IPaginatedRequest';
import { IPatientProviderListData } from '../../../../models/request/IPatientProviderListData';
import { IPaginatedResponse } from '../../../../models/response/IPaginatedResponse';
import { IResponse } from '../../../../models/response/IResponse';
import { IStatusCount } from '../../../../models/response/IStatusCounts';
import { AdminService } from '../../../../services/admin/admin.service';
import { ModalService } from '../../../../services/modal/modal.service';
import { NotificationService } from '../../../../shared/services/notification.service';
import { SystemConstants } from '../../../../constants/system-constants/system-constants';

@Component({
  selector: 'app-provider-list',
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
  templateUrl: './provider-list.component.html',
  styleUrl: './provider-list.component.scss',
})
export class ProviderListComponent {
  userType: number = SystemConstants.ProviderUser;
  activeCard: string = 'All';
  datasource: IPatientProviderListData[] = [];
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
    private notificationService: NotificationService,
    private router: Router,
    private modalService: ModalService
  ) {}

  ngOnInit() {
    this.searchSubject.pipe(debounceTime(1000)).subscribe((searchQuery) => {
      this.search(searchQuery);
    });
    this.getProviderListRequest();
    this.getStatusCount();
  }

  onKeyup(searchQuery: string) {
    this.searchSubject.next(searchQuery);
  }

  search(searchQuery: string) {
    this.searchQuery = searchQuery;
    this.getProviderListRequest();
  }

  setActiveCard(card: string) {
    this.activeCard = card;
    this.status = card === 'All' ? '' : card;
    this.pageIndex = 1;
    this.getProviderListRequest();
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
    this.getProviderListRequest();
  }

  getProviderListRequest() {
    const request: IPaginatedRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      sortOrder: this.sortOrder,
      sortColumn: this.sortColumn,
      searchQuery: this.searchQuery,
      status: this.status,
    };

    this.adminService.getPatientProviderList(request, this.userType).subscribe({
      next: (
        response: IResponse<IPaginatedResponse<IPatientProviderListData[]>>
      ) => {
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
    this.adminService.statusCountList(this.userType).subscribe({
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

  viewCase(userId: number): void {
    this.router.navigate(['admin-dashboard/view-case', userId]);
  }

  openBlockCase(userId: number, name: string) {
    this.modalService.openBlockCase(userId, name);
  }
}
