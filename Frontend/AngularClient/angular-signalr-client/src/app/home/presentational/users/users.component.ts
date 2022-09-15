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
  selectedUsers: string[] = [];
  userConnectionId: string = this.signalrService.getConnectionId() as string;

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
      this.allUsers = users.allClients;
    })
  }
  
  notifyUser(toUser: string, message: string) {
    const notification: IUserNotification = {
      senderId: this.signalrService.getConnectionId() as string,      
      userId: toUser,
      message: message
    };
    console.log(notification)
    this.usersService.notifyUser(notification).subscribe();
  }

  notifyAllUsers(message: string) {
    const notification: IUserNotification = {
      senderId: this.signalrService.getConnectionId() as string,
      message: message
    };
    this.usersService.notifyAllUsers(notification).subscribe();
  }

  notifySelectedUsers(selctedUsers: string[], message: string) {
    const notification: IUserNotification = {
      senderId: this.signalrService.getConnectionId() as string,
      userIds: selctedUsers,
      message: message
    };
    this.usersService.notifySelectedUsers(notification).subscribe();
  }
  
  addToSelectedUserIds(userId: string): void {
    this.selectedUsers.push(userId);
  }

  removeFromSelectedUserIds(userId: string): void {
    const index = this.selectedUsers.indexOf(userId);
    if (index !== -1) {
      this.selectedUsers.splice(index, 1);
    }
  }
}
