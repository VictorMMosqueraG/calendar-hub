import { Component, computed, input } from '@angular/core';
import { CalendarEvent } from '../../models/calendar-event.model';

@Component({
  selector: 'app-event-card',
  standalone: true,
  templateUrl: './event-card.html',
  styleUrl: './event-card.scss',
})
export class EventCardComponent {
  event = input.required<CalendarEvent>();

  timeRange = computed(() => {
    const e = this.event();
    const start = new Date(e.start);
    const end = new Date(e.end);
    return `${this.formatTime(start)} - ${this.formatTime(end)}`;
  });

  private formatTime(date: Date): string {
    return date.toLocaleTimeString('es-CO', { hour: '2-digit', minute: '2-digit', hour12: false });
  }
}
