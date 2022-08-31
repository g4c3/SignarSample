import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAllGroups, IGroupManagement, } from '../models';

@Injectable({ providedIn: 'root' })
export class GoupService {

  constructor(private readonly httpClient: HttpClient) { }

  getAllGroups(): Observable<IAllGroups> {
    return this.httpClient.get<IAllGroups>(`groups/getallgroups`);
  }

  createGroup(group: IGroupManagement): Observable<unknown> {
    return this.httpClient.post(`groups/creategroup`, { group });
  }

  leaveGroup(group: IGroupManagement): Observable<unknown> {
    return this.httpClient.post(`groups/leavegroup`, { group });
  }
}
