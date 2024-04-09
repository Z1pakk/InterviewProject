export interface IFilterCommand {
  take?: number;
  skip?: number;
  sortOrder?: number;
  sortField?: string;
  searchQuery?: string;
}
