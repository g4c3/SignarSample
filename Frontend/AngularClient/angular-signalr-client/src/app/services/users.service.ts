import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUserNotification, IAllUsers } from '../models';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private readonly httpClient: HttpClient) { }

  getAllClientIds(): Observable<IAllUsers> {
    return this.httpClient.get<IAllUsers>(`users/getallclientids`);
  }

  notifyUser(notification: IUserNotification): Observable<unknown> {
    return this.httpClient.post(`users/notifyuser`, notification);
  }
  
  notifyAllUsers(notification: IUserNotification): Observable<unknown> {
    return this.httpClient.post(`users/notifyallusers`, notification);
  }

  notifySelectedUsers(notification: IUserNotification): Observable<unknown> {
    return this.httpClient.post(`users/notifselectedlusers`, notification);
  }
}
