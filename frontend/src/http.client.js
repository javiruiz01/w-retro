import { updateCards } from './Hub.js';
import { sessionStore } from './Stores/SessionStore.js';

let roomId = '';
sessionStore.subscribe((value) => void (roomId = value));

const { API_URL: baseUrl } = process.env;
const headers = { 'Content-type': 'application/json' };

export async function createSession() {
  const url = `${baseUrl}/session`;
  return await fetch(url, { method: 'POST' }).then((res) => res.json());
}

export async function fetchCards() {
  const url = `${baseUrl}/session/${roomId}`;
  return await fetch(url).then((res) => res.json());
}

export async function postComment(cardId, text) {
  const url = `${baseUrl}/cards/${cardId}`;
  const body = { text };
  return await fetch(url, {
    method: 'POST',
    body: JSON.stringify(body),
    headers,
  }).then((res) => {
    updateCards(roomId);
    return res.json();
  });
}

export async function removeComment(commentId) {
  const url = `${baseUrl}/comments/${commentId}`;
  await fetch(url, { method: 'DELETE', headers }).then(
    (_) => void updateCards(roomId)
  );
}

export async function updateComment(comment) {
  const url = `${baseUrl}/comments/${comment.id}`;
  await fetch(url, {
    method: 'PUT',
    body: JSON.stringify(comment),
    headers,
  }).then((_) => void updateCards(roomId));
}

export async function updateTitle(cardId, title) {
  const url = `${baseUrl}/cards/${cardId}`;
  const body = { text: title };
  await fetch(url, { method: 'PUT', body: JSON.stringify(body), headers }).then(
    (_) => void updateCards(roomId)
  );
}

export async function addColumn(position) {
  const url = `${baseUrl}/cards`;
  const body = { position, sessionId: roomId };
  return await fetch(url, {
    method: 'POST',
    body: JSON.stringify(body),
    headers,
  }).then((res) => {
    updateCards(roomId);
    return res.json();
  });
}

export async function removeColumn(cardId) {
  const url = `${baseUrl}/cards/${cardId}`;
  await fetch(url, { method: 'DELETE', headers }).then(
    (_) => void updateCards(roomId)
  );
}
