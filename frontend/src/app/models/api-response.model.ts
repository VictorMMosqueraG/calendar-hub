export interface ApiResponse<T = void> {
  message?: string;
  results?: T;
}
