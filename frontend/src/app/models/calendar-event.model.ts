export interface CalendarEvent {
  id: string;
  title: string;
  start: string;
  end: string;
  provider: string;
  account: string;
  location: string | null;
  description: string | null;
  is_all_day: boolean;
}
