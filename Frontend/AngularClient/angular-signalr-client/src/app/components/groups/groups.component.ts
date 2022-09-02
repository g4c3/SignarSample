import { Component, OnInit } from '@angular/core';
import { GroupService } from '../../services/group.service';
import { SignalrService } from 'src/app/services';
import { IAllGroups } from 'src/app/models';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent implements OnInit {
  allGroups: string[] | undefined;
  constructor(
    private readonly groupService: GroupService,
    private readonly signalrService: SignalrService,
  ) {}

  ngOnInit(): void {
    this.signalrService.initializeHubConnection();
  }
  getGroups(): void{
    this.groupService.getAllGroups().subscribe((groups: IAllGroups) => {
      this.allGroups = groups.allGroups;
      console.log(this.allGroups);
    });
  }
}
