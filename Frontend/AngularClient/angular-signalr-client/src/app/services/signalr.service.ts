import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  connectionEstablished$ = new BehaviorSubject<boolean>(false);
  incomingMessage$ = new Subject<string>();
  private hubConnection: signalR.HubConnection | undefined;

  constructor() {}

  initializeHubConnection(): Promise<any> {
    return new Promise((resolve, reject) => {
      if (this.hubConnection !== undefined) {
        throw new Error('the signalr connection has already been initialized.');
      }
      const hubUrl = 'http://localhost:5142/CommunicationHub';
      const logLevel = signalR.LogLevel.Debug;
  
      this.hubConnection = new signalR.HubConnectionBuilder()    
      .configureLogging(logLevel)
      .withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();      
       
      this.hubConnection
      .start()
      .then(() => { 
        console.log('Connection started');
        resolve(true);
      })
      .catch(err => {
        console.log('Error while starting connection: ' + err);
        reject();
      });

      this.setSignalrClientMethods();
    })
  }

  private setSignalrClientMethods(): void {
    this.hubConnection?.on('MessageToUser', (message: string) => {
      // console.log('message ' + message);
      this.incomingMessage$.next(message);
    });

    this.hubConnection?.on('MessageToAllUsers', (message: string) => {
      // console.log('message ' + message);
      this.incomingMessage$.next(message);
    });

    this.hubConnection?.on('MessageToUsers', (message: string) => {
      // console.log('message ' + message);
      this.incomingMessage$.next(message);
    });
    
    this.hubConnection?.on('MessageToGroup', (message: string) => {
      // console.log('message ' + message);
      this.incomingMessage$.next(message);
    });
    
    this.hubConnection?.on('MessageToGroups', (message: string) => {
      // console.log('message ' + message);
      this.incomingMessage$.next(message);
    });
  }

  public getConnectionId(): string | undefined {
    if(this.hubConnection?.connectionId != null || this.hubConnection?.connectionId != undefined)
      return this.hubConnection?.connectionId as string;
    else
      return '';  
  }

  
}
