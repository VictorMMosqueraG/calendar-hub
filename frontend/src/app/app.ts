import { Component, inject, OnInit, signal } from '@angular/core';
import { HeaderComponent } from './components/header/header';
import { DayViewComponent } from './components/day-view/day-view';
import { CalendarApiService } from './services/calendar-api.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeaderComponent, DayViewComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  private api = inject(CalendarApiService);

  isGoogleConnected = signal(false);

  ngOnInit() {
    this.checkStatus();
  }

  onConnect() {
    this.api.connectGoogle();
  }

  onDisconnect() {
    this.api.disconnectGoogle().subscribe(() => {
      this.isGoogleConnected.set(false);
    });
  }

  private checkStatus() {
    this.api.getAuthStatus().subscribe({
      next: (status) => {
        this.isGoogleConnected.set(status['google'] === true || status['Google'] === true);
      },
      error: () => {
        this.isGoogleConnected.set(false);
      },
    });
  }
}
