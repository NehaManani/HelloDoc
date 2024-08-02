import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BlockCaseComponent } from '../../pages/admin/admin-dashboard/patient-list/block-case/block-case.component';

@Injectable({
  providedIn: 'root',
})


export class ModalService {
  constructor(private modalService: NgbModal) {}

  openBlockCase(userId: number, name: string) {
    const modalRef = this.modalService.open(BlockCaseComponent, {
      centered: true,
    });
    modalRef.componentInstance.userName = name;
    modalRef.componentInstance.userId = userId;
  }
}
