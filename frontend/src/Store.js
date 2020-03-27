import { writable } from 'svelte/store';

const cards = [
  {
    title: 'Good',
    comments: [{ text: 'So much work!' }],
  },
  {
    title: 'Meh',
    comments: [{ text: 'Innovation week canceled' }],
  },
  {
    title: 'Bad',
    comments: [{ text: 'Something about infra' }],
  },
];
export const data = writable(cards);
