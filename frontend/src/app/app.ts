import { Component, inject, OnInit, signal } from '@angular/core';
import { Router, NavigationEnd, RouterOutlet } from '@angular/router';
import { filter } from 'rxjs';
import { HeaderComponent } from './components/header/header';
import { DayViewComponent } from './components/day-view/day-view';
import { CalendarApiService } from './services/calendar-api.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, DayViewComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  private api = inject(CalendarApiService);
  private router = inject(Router);

  isGoogleConnected = signal(false);

  ngOnInit() {
    this.checkStatus();

    this.router.events
      .pipe(filter((e) => e instanceof NavigationEnd && e.urlAfterRedirects === '/'))
      .subscribe(() => this.checkStatus());
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
