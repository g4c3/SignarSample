import { Component, OnInit } from '@angular/core';
import { SignalrService } from 'src/app/services';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  chatmessages : string[] = [];

  constructor(
    private readonly signalrService: SignalrService,
  ) { }

  ngOnInit(): void {
    this.signalrService.incomingMessage$.subscribe((message) => {
      this.chatmessages = [...this.chatmessages, message];
    })
  }

  
}
