import { Component, computed, effect, inject, input, signal } from '@angular/core';
import { CalendarEvent } from '../../models/calendar-event.model';
import { CalendarApiService } from '../../services/calendar-api.service';
import { EventCardComponent } from '../event-card/event-card';

@Component({
  selector: 'app-day-view',
  standalone: true,
  imports: [EventCardComponent],
  templateUrl: './day-view.html',
  styleUrl: './day-view.scss',
})
export class DayViewComponent {
  private api = inject(CalendarApiService);

  isConnected = input<boolean>(false);

  fromDate = signal<Date>(new Date());
  toDate = signal<Date>(new Date());
  events = signal<CalendarEvent[]>([]);
  isLoading = signal(false);

  fromInputValue = computed(() => this.toInputDate(this.fromDate()));
  toInputValue = computed(() => this.toInputDate(this.toDate()));

  groupedEvents = computed(() => {
    const groups: { date: string; label: string; events: CalendarEvent[] }[] = [];
    const eventList = this.events();

    for (const event of eventList) {
      const date = new Date(event.start);
      const key = date.toISOString().split('T')[0];
      const existing = groups.find(g => g.date === key);
      if (existing) {
        existing.events.push(event);
      } else {
        const label = date.toLocaleDateString('es-CO', {
          weekday: 'long', year: 'numeric', month: 'long', day: 'numeric',
        });
        groups.push({
          date: key,
          label: label.charAt(0).toUpperCase() + label.slice(1),
          events: [event],
        });
      }
    }

    return groups.sort((a, b) => a.date.localeCompare(b.date));
  });

  constructor() {
    effect(() => {
      if (!this.isConnected()) {
        this.events.set([]);
        return;
      }
      this.loadEvents();
    });
  }

  onFromChange(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    if (value) {
      const [y, m, d] = value.split('-').map(Number);
      this.fromDate.set(new Date(y, m - 1, d));
    }
  }

  onToChange(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    if (value) {
      const [y, m, d] = value.split('-').map(Number);
      this.toDate.set(new Date(y, m - 1, d));
    }
  }

  search() {
    this.loadEvents();
  }

  goToToday() {
    this.fromDate.set(new Date());
    this.toDate.set(new Date());
    this.loadEvents();
  }

  private loadEvents() {
    const from = new Date(this.fromDate());
    from.setHours(0, 0, 0, 0);

    const to = new Date(this.toDate());
    to.setHours(23, 59, 59, 999);

    this.isLoading.set(true);
    this.api.getEvents(from, to).subscribe({
      next: (events) => {
        this.events.set(events);
        this.isLoading.set(false);
      },
      error: () => {
        this.events.set([]);
        this.isLoading.set(false);
      },
    });
  }

  private toInputDate(d: Date): string {
    const y = d.getFullYear();
    const m = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${y}-${m}-${day}`;
  }
}
