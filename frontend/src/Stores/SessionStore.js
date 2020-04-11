import { writable } from 'svelte/store';

export const emptySession = { id: '', title: '' };
export const sessionStore = writable(emptySession);
