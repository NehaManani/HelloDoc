import { HttpClient } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiCallConstant } from '../../constants/api-call/api';
import { IResponse } from '../../models/response/IResponse';
import { IPaginatedResponse } from '../../models/response/IPaginatedResponse';
import { IPatientListData } from '../../models/request/IPatientListData';
import { IPaginatedRequest } from '../../models/request/IPaginatedRequest';
import { IStatusCount } from '../../models/response/IStatusCounts';
import { IPatientDetailsResponse } from '../../models/response/IPatientDetailsResponse';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BlockCaseComponent } from '../../pages/admin/admin-dashboard/patient-list/block-case/block-case.component';
import { IBlockCase } from '../../models/request/IBlockCase';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  getPatientListApi = ApiCallConstant.GET_PATIENT_LIST;
  getStatusCountListApi = ApiCallConstant.GET_STATUS_COUNT_LIST;
  getPatientDetailsByIdApi = ApiCallConstant.GET_PATIENT_DETAILS;

  blockCaseApi = ApiCallConstant.BLOCK_CASE;
  constructor(private http: HttpClient) {}

  getPatientList(
    request: IPaginatedRequest
  ): Observable<IResponse<IPaginatedResponse<IPatientListData[]>>> {
    return this.http.post<IResponse<IPaginatedResponse<IPatientListData[]>>>(
      this.getPatientListApi,
      request
    );
  }

  statusCountList(): Observable<IResponse<IStatusCount>> {
    return this.http.get<IResponse<IStatusCount>>(this.getStatusCountListApi);
  }

  getPatientDetails(
    userId: number
  ): Observable<IResponse<IPatientDetailsResponse>> {
    return this.http.get<IResponse<IPatientDetailsResponse>>(
      `${this.getPatientDetailsByIdApi}?userId=${userId}`
    );
  }

  blockCase(request: IBlockCase): Observable<IResponse<null>> {
    return this.http.post<IResponse<null>>(this.blockCaseApi, request);
  }
}
