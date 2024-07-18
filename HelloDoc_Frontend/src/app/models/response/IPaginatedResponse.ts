export interface IPaginatedResponse<T> {
  pageIndex: number;
  pageSize: number;
  totalRecords: number;
  records: T;
}
