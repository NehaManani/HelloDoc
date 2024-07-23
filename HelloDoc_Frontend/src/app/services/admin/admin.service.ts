import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiCallConstant } from '../../constants/api-call/api';
import { IResponse } from '../../models/response/IResponse';
import { IPaginatedResponse } from '../../models/response/IPaginatedResponse';
import { IPatientData } from '../../models/request/IPatientData';
import { IPaginatedRequest } from '../../models/request/IPaginatedRequest';
import { IStatusCount } from '../../models/response/IStatusCounts';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  getPatientListApi = ApiCallConstant.GET_PATIENT_LIST;
  getStatusCountListApi = ApiCallConstant.GET_STATUS_COUNT_LIST;
  constructor(private http: HttpClient) {}

  getPatientList(
    request: IPaginatedRequest
  ): Observable<IResponse<IPaginatedResponse<IPatientData[]>>> {
    return this.http.post<IResponse<IPaginatedResponse<IPatientData[]>>>(
      this.getPatientListApi,
      request
    );
  }

  statusCountList(): Observable<IResponse<IStatusCount>> {
    return this.http.get<IResponse<IStatusCount>>(this.getStatusCountListApi);
  }
}
