import { hubClient } from './hub.js';
import { sessionStore } from './Stores/SessionStore.js';

let roomId = '';
sessionStore.subscribe(({ id }) => void (roomId = id));

const { API_URL: baseUrl } = process.env;
const headers = { 'Content-type': 'application/json' };

export const httpClient = {
  addColumn,
  createSession,
  fetchSession,
  postComment,
  removeColumn,
  removeComment,
  updateComment,
  updateSessionTitle,
  updateTitle,
};

async function createSession() {
  const url = `${baseUrl}/session`;
  return await fetch(url, { method: 'POST' }).then((res) => res.json());
}

async function fetchSession() {
  const url = `${baseUrl}/session/${roomId}`;
  return await fetch(url)
    .then((res) => {
      if (!res.ok) throw new Error();
      return res.json();
    })
    .catch((_) => null);
}

async function postComment(cardId, text) {
  const url = `${baseUrl}/cards/${cardId}`;
  const body = { text };
  return await fetch(url, {
    method: 'POST',
    body: JSON.stringify(body),
    headers,
  }).then((res) => {
    hubClient.updateSession(roomId);
    return res.json();
  });
}

async function removeComment(commentId) {
  const url = `${baseUrl}/comments/${commentId}`;
  await fetch(url, { method: 'DELETE', headers }).then(
    (_) => void hubClient.updateSession(roomId)
  );
}

async function updateComment(comment) {
  const url = `${baseUrl}/comments/${comment.id}`;
  await fetch(url, {
    method: 'PUT',
    body: JSON.stringify(comment),
    headers,
  }).then((_) => void hubClient.updateSession(roomId));
}

async function updateTitle(cardId, title) {
  const url = `${baseUrl}/cards/${cardId}`;
  const body = { text: title };
  await fetch(url, { method: 'PUT', body: JSON.stringify(body), headers }).then(
    (_) => void hubClient.updateSession(roomId)
  );
}

async function addColumn(position) {
  const url = `${baseUrl}/cards`;
  const body = { position, sessionId: roomId };
  return await fetch(url, {
    method: 'POST',
    body: JSON.stringify(body),
    headers,
  }).then((res) => {
    hubClient.updateSession(roomId);
    return res.json();
  });
}

async function removeColumn(cardId) {
  const url = `${baseUrl}/cards/${cardId}`;
  await fetch(url, { method: 'DELETE', headers }).then(
    (_) => void hubClient.updateSession(roomId)
  );
}

async function updateSessionTitle(title) {
  const url = `${baseUrl}/session/${roomId}`;
  const body = { text: title };
  await fetch(url, {
    method: 'PUT',
    body: JSON.stringify(body),
    headers,
  }).then((_) => void hubClient.updateSession(roomId));
}
