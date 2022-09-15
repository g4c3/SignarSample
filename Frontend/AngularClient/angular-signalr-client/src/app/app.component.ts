import { Component } from '@angular/core';
import { SignalrService } from 'src/app/services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'angular-signalr-client';

  constructor(private readonly signalRService: SignalrService) {}
}
