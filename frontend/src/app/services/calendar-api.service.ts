import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CalendarEvent } from '../models/calendar-event.model';

@Injectable({ providedIn: 'root' })
export class CalendarApiService {
  private http = inject(HttpClient);

  getAuthStatus(): Observable<Record<string, boolean>> {
    return this.http.get<Record<string, boolean>>('/api/auth/status');
  }

  getEvents(from: Date, to: Date): Observable<CalendarEvent[]> {
    const params = {
      from: from.toISOString(),
      to: to.toISOString(),
    };
    return this.http.get<CalendarEvent[]>('/api/calendar/events', { params });
  }

  disconnectGoogle(): Observable<unknown> {
    return this.http.delete('/api/auth/google');
  }

  connectGoogle(): void {
    window.location.href = '/api/auth/google';
  }
}
