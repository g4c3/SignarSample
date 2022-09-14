import { Component, OnInit } from '@angular/core';
import { SignalrService } from 'src/app/services';
import { UsersService } from 'src/app/services';
import { IAllUsers, IUserNotification } from 'src/app/models';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  chatmessages : string[] = [];
  allUsers: string[] = [];

  constructor(
    private readonly signalrService: SignalrService,
    private readonly usersService: UsersService,

  ) { }

  ngOnInit(): void {
    this.signalrService.incomingMessage$.subscribe((message) => {
      this.chatmessages = [...this.chatmessages, message];
    })
  }

  getUsers() {
    this.usersService.getAllClientIds().subscribe((users: IAllUsers) => {
      this.allUsers = users.allusers;
    })
  }
  
  notifyUser(message: string) {
    const notification: IUserNotification = {
      senderId: this.signalrService.getConnectionId() as string,
      message: message
    };
    this.usersService.notifyUser(notification);
  }

  notifyAllUsers(message: string) {
    const notification: IUserNotification = {
      senderId: this.signalrService.getConnectionId() as string,
      message: message
    };
    this.usersService.notifyAllUsers(notification);
  }

  notifySelectedUsers(message: string, selctedUsers: string[]) {
    const notification: IUserNotification = {
      senderId: this.signalrService.getConnectionId() as string,
      userIds: selctedUsers,
      message: message
    };
    this.usersService.notifySelectedUsers(notification);
  }
}
