import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { CalendarEvent } from '../models/calendar-event.model';
import { ApiResponse } from '../models/api-response.model';

@Injectable({ providedIn: 'root' })
export class CalendarApiService {
  private http = inject(HttpClient);

  getAuthStatus(): Observable<Record<string, boolean>> {
    return this.http
      .get<ApiResponse<{ providers: Record<string, boolean> }>>('/api/auth/status')
      .pipe(map((res) => res.results?.providers ?? {}));
  }

  getEvents(from: Date, to: Date): Observable<CalendarEvent[]> {
    const params = {
      from: from.toISOString(),
      to: to.toISOString(),
    };
    return this.http
      .get<ApiResponse<CalendarEvent[]>>('/api/calendar/events', { params })
      .pipe(map((res) => res.results ?? []));
  }

  disconnectGoogle(): Observable<unknown> {
    return this.http.delete<ApiResponse>('/api/auth/google');
  }

  connectGoogle(): void {
    this.http
      .get<ApiResponse<{ url: string }>>('/api/oauth/google')
      .subscribe((res) => {
        if (res.results?.url) {
          window.location.href = res.results.url;
        }
      });
  }

  exchangeToken(provider: string, code: string): Observable<unknown> {
    return this.http.post<ApiResponse>(`/api/oauth/${provider}/callback`, {
      code,
    });
  }
}
