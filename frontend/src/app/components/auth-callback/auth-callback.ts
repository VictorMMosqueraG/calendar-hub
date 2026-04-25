import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CalendarApiService } from '../../services/calendar-api.service';

@Component({
  selector: 'app-auth-callback',
  standalone: true,
  template: '<p>Connecting...</p>',
})
export class AuthCallbackComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private api = inject(CalendarApiService);

  ngOnInit() {
    const code = this.route.snapshot.queryParamMap.get('code');
    const provider = this.route.snapshot.paramMap.get('provider');

    console.log('OAuth callback - provider:', provider, 'code:', code);
    if (code && provider) {
      this.api.exchangeToken(provider, code).subscribe({
        next: () => this.router.navigate(['/']),
        error: () => this.router.navigate(['/']),
      });
    } else {
      this.router.navigate(['/']);
    }
  }
}
