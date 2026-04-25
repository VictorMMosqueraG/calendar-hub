import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class HeaderComponent {
  isConnected = input<boolean>(false);
  connect = output<void>();
  disconnect = output<void>();
}
