import { HttpClient } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiCallConstant } from '../../constants/api-call/api';
import { IResponse } from '../../models/response/IResponse';
import { IPaginatedResponse } from '../../models/response/IPaginatedResponse';
import { IPaginatedRequest } from '../../models/request/IPaginatedRequest';
import { IStatusCount } from '../../models/response/IStatusCounts';
import { IPatientDetailsResponse } from '../../models/response/IPatientDetailsResponse';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BlockCaseComponent } from '../../pages/admin/admin-dashboard/patient-list/block-case/block-case.component';
import { IBlockCase } from '../../models/request/IBlockCase';
import { IPatientProviderListData } from '../../models/request/IPatientProviderListData';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  getPatientProviderListApi = ApiCallConstant.GET_PATIENT_PROVIDER_LIST;
  getStatusCountListApi = ApiCallConstant.GET_STATUS_COUNT_LIST;
  getPatientDetailsByIdApi = ApiCallConstant.GET_PATIENT_DETAILS;

  blockCaseApi = ApiCallConstant.BLOCK_CASE;

  constructor(private http: HttpClient) {}

  getPatientProviderList(
    request: IPaginatedRequest,
    userType: number
  ): Observable<IResponse<IPaginatedResponse<IPatientProviderListData[]>>> {
    return this.http.post<
      IResponse<IPaginatedResponse<IPatientProviderListData[]>>
    >(`${this.getPatientProviderListApi}?userType=${userType}`, request);
  }

  statusCountList(userType: number): Observable<IResponse<IStatusCount>> {
    return this.http.get<IResponse<IStatusCount>>(
      `${this.getStatusCountListApi}?userType=${userType}`
    );
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
