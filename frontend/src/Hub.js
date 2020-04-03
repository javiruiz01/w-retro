import * as signalR from '@aspnet/signalr';
import { cards } from './Stores/CardsStore.js';

const baseUrl = 'http://localhost:8080';
let hubConnection;

export const initHubConnection = () => {
  hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${baseUrl}/realtime`)
    .build();
  hubConnection
    .start()
    .then(() => console.log('Connected to server'))
    .catch(console.error);

  hubConnection.on('Connected', console.log);
  hubConnection.on('Update', (res) => void cards.set(res.cards));
};

export const addToGroup = (session) =>
  void hubConnection.invoke('AddToGroup', session);

export const updateCards = (session) =>
  void hubConnection.invoke('UpdateCards', session);
