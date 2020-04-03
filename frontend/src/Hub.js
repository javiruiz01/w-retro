import * as signalR from '@aspnet/signalr';
import { baseUrl } from './http.client';
let hubConnection;

export const initHubConnection = () => {
  hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${baseUrl}/realtime`)
    .build();
  hubConnection
    .start()
    .then(() => console.log('Connected to server'))
    .catch(console.error);
  hubConnection.on('Connected', (res) => console.log(res));
};

export const addToGroup = (session) =>
  void hubConnection
    .invoke('AddToGroup', session)
    .then(() => console.log('Added to group!'));
