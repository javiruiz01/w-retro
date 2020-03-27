import { data } from './Store.js';

const baseUrl = 'http://localhost:8080';

export async function fetchCards(id) {
  const url = `${baseUrl}/cards/${id}`;
  await fetch(url)
    .then((res) => res.json())
    .then((res) => data.update((value) => (value = res)));
}

export async function postComment(id, idx, comment) {
  const url = `${baseUrl}/cards/${id}`;
  const body = { cardIdx: idx, comment };
  const headers = {
    'Content-type': 'application/json',
  };
  await fetch(url, { method: 'POST', body: JSON.stringify(body), headers })
    .then((res) => res.json())
    .then((res) => {
      console.log(res);
    });
}
