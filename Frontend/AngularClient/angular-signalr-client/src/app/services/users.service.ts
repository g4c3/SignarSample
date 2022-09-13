import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUserNotification } from '../models';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private readonly httpClient: HttpClient) { }

  getAllClientIds(): Observable<string[]> {
    return this.httpClient.get<string[]>(`users/getallclientids`);
  }

  notifyUser(notification: IUserNotification): Observable<unknown> {
    return this.httpClient.post(`users/notifyuser`, notification);
  }
  
  //abstracted interfaces
  notifyAllUsers(notification: IUserNotification): Observable<unknown> {
    return this.httpClient.post(`users/notifyallusers`, notification);
  }

  //abstracted interfaces
  notifySelectedUsers(notification: IUserNotification): Observable<unknown> {
    return this.httpClient.post(`users/notifselectedlusers`, notification);
  }
}
