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
  private hubUrl = 'http://localhost:5142/CommunicationHub';
  private logLevel = signalR.LogLevel.Debug;

  constructor() {
    this.createSignalrConnection();
    this.setSignalrClientMethods();
    this.startConnection();
  }

  private createSignalrConnection() {
    if(this.hubConnection === undefined){
      console.log("connection undefined")
    } else {
      console.log("connection exists")

    }
    this.hubConnection = new signalR.HubConnectionBuilder()    
      .configureLogging(this.logLevel)
      .withUrl(this.hubUrl)
      .withAutomaticReconnect()
      .build();
  }

  private setSignalrClientMethods(): void {
    this.hubConnection?.on('MessageToUser', (message: string) => {
      this.incomingMessage$.next(message);
    });

    this.hubConnection?.on('MessageToAllUsers', (message: string) => {
      this.incomingMessage$.next(message);
    });

    this.hubConnection?.on('MessageToUsers', (message: string) => {
      this.incomingMessage$.next(message);
    });
    
    this.hubConnection?.on('MessageToGroup', (message: string) => {
      this.incomingMessage$.next(message);
    });
    
    this.hubConnection?.on('MessageToGroups', (message: string) => {
      this.incomingMessage$.next(message);
    });
  }

  private startConnection(): Promise<any> {
    return new Promise((resolve, reject) => {
      if (this.hubConnection!.state === signalR.HubConnectionState.Connected) {
        return;
      }
      this.hubConnection!
        .start()
        .then(() => { 
          console.log('Connection started');
          resolve(true);
        })
        .catch(err => {
          console.log('Error while starting connection: ' + err);
          reject();
        });
    })
  }

  public getConnectionId(): string | undefined {
    if(this.hubConnection?.connectionId != null || this.hubConnection?.connectionId != undefined)
      return this.hubConnection?.connectionId as string;
    else
      return '';  
  }
}
