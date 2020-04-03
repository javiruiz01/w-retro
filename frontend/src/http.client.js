import { session } from './SessionStore.js';

let roomId = '';
session.subscribe((value) => void (roomId = value));

export const baseUrl = 'http://localhost:8080';
const headers = { 'Content-type': 'application/json' };

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
  }).then((res) => res.json());
}

export async function removeComment(commentId) {
  const url = `${baseUrl}/comments/${commentId}`;
  await fetch(url, { method: 'DELETE', headers });
}

export async function updateComment(comment) {
  const url = `${baseUrl}/comments/${comment.id}`;
  await fetch(url, {
    method: 'PUT',
    body: JSON.stringify(comment),
    headers,
  });
}
