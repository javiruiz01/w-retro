import { data } from './Store.js';

const baseUrl = 'http://localhost:8080';
let roomId = '8986E7EF-6FF4-49EC-BAEA-EFC21576D649';

export async function fetchCards() {
  const url = `${baseUrl}/session/${roomId}`;
  await fetch(url)
    .then((res) => res.json())
    .then((res) => data.update((value) => (value = res.cards)));
}

export async function postComment(cardIdx, comment) {
  const url = `${baseUrl}/comment/${roomId}/${cardIdx}`;
  const body = { text: comment };
  const headers = { 'Content-type': 'application/json' };
  return await fetch(url, {
    method: 'POST',
    body: JSON.stringify(body),
    headers,
  }).then((res) => res.json());
}

export async function removeComment(cardIdx, commentId) {
  const url = `${baseUrl}/comment/${roomId}/${cardIdx}/${commentId}`;
  const headers = { 'Content-type': 'application/json' };
  await fetch(url, { method: 'DELETE', headers })
    .then((res) => res.json())
    .then((res) => console.log(res));
}

export async function updateComment(cardIdx, commentIdx, comment) {
  const url = `${baseUrl}/comment/${roomId}/${cardIdx}/${commentIdx}`;
  const headers = { 'Content-type': 'application/json' };
  await fetch(url, { method: 'POST', body: JSON.stringify(comment), headers })
    .then((res) => res.json())
    .then(console.log);
}
