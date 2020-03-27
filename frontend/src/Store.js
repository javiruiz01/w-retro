import { writable } from 'svelte/store';

export const data = writable([]);

fetch('http://localhost:8080/cards')
  .then((res) => res.json())
  .then((res) => data.update((value) => (value = res)));
