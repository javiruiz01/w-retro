import * as signalR from '@microsoft/signalr';
import { cardsStore } from './Stores/CardsStore.js';

const { API_URL: baseUrl } = process.env;
let hubConnection;

export const hubClient = {
  initHubConnection,
  addToGroup,
  updateSession,
};

function initHubConnection() {
  hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${baseUrl}/realtime`)
    .build();

  return hubConnection
    .start()
    .then(() => {
      hubConnection.on('Connected', console.log);
      hubConnection.on('Update', ({ cards }) => void cardsStore.set(cards));
    })
    .catch(console.error);
}

function addToGroup(session) {
  hubConnection.invoke('AddToGroup', session);
}

function updateSession(session) {
  hubConnection.invoke('UpdateSession', session);
}
