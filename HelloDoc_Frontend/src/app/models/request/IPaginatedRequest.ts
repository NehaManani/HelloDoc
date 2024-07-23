export interface IPaginatedRequest {
  pageIndex: number;
  pageSize: number;
  sortOrder: string;
  sortColumn: string;
  searchQuery: string;
  status: string;
}
