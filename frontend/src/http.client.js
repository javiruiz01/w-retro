const baseUrl = 'http://localhost:8080';
let roomId = '8986E7EF-6FF4-49EC-BAEA-EFC21576D649';

export async function fetchCards() {
  const url = `${baseUrl}/session/${roomId}`;
  return await fetch(url).then((res) => res.json());
}

export async function postComment(cardIdx, comment) {
  const url = `${baseUrl}/cards/${cardIdx}`;
  const body = { text: comment };
  const headers = { 'Content-type': 'application/json' };
  return await fetch(url, {
    method: 'POST',
    body: JSON.stringify(body),
    headers,
  }).then((res) => res.json());
}

export async function removeComment(commentId) {
  const url = `${baseUrl}/comments/${commentId}`;
  const headers = { 'Content-type': 'application/json' };
  await fetch(url, { method: 'DELETE', headers }).then((res) => res.json());
}

export async function updateComment(comment) {
  const url = `${baseUrl}/comments/${comment.id}`;
  const headers = { 'Content-type': 'application/json' };
  await fetch(url, {
    method: 'PUT',
    body: JSON.stringify(comment),
    headers,
  }).then((res) => res.text());
}
