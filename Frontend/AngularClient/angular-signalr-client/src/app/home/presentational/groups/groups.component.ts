import { Component, OnInit } from '@angular/core';
import { GroupService } from 'src/app/services/group.service';
import { SignalrService } from 'src/app/services';
import { IAllGroups, IGroupManagement } from 'src/app/models';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {
  allGroups: string[] | undefined;
  groupToLeave: string = '';
  chatmessages : string[] = [];

  constructor(
    private readonly groupService: GroupService,
    private readonly signalrService: SignalrService,
  ) { }

  ngOnInit(): void {
    this.signalrService.incomingMessage$.subscribe((message) => {
      this.chatmessages = [...this.chatmessages, message];
      console.log(this.chatmessages);
    })
  }
  getGroups(): void{
    this.groupService.getAllGroups().subscribe((groups: IAllGroups) => {
      this.allGroups = groups.allGroups;
    });
  }
  leaveGroup(groupName: string): void {
    const group: IGroupManagement = {
      connectionId: this.signalrService.getConnectionId() as string,
      groupName: groupName
    };

    this.groupService.leaveGroup(group).subscribe();
  }
  joinGroup(groupName: string): void {
    const group: IGroupManagement = {
      connectionId: this.signalrService.getConnectionId() as string,
      groupName: groupName
    };
    this.groupService.createGroup(group).subscribe();
  }
}
