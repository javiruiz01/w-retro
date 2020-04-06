import * as signalR from '@microsoft/signalr';
import { cardsStore } from './Stores/CardsStore.js';

const { API_URL: baseUrl } = process.env;
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
  hubConnection.on('Update', ({ cards }) => void cardsStore.set(cards));
};

export const addToGroup = (session) =>
  void hubConnection.invoke('AddToGroup', session);

export const updateCards = (session) =>
  void hubConnection.invoke('UpdateCards', session);
