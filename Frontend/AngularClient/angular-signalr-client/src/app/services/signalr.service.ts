import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection: signalR.HubConnection | undefined;
  private clientMessage: string | undefined;

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
      console.log('message ' + message);
      this.clientMessage =message;
    });

    this.hubConnection?.on('MessageToAllUsers', (message: string) => {
      console.log('message ' + message);
      this.clientMessage = message;
    });
  }
}
