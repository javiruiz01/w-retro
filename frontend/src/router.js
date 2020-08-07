import { writable } from 'svelte/store';

export const currentPath = writable(getPathname());

export const router = {
  onLocationChange,
  navigate,
  getPathname,
};

function onLocationChange(callback) {
  window.addEventListener('popstate', callback);
}

function navigate(path) {
  window.history.pushState({ path }, '', `${window.location.origin}/${path}`);
  currentPath.set(path);
}

function getPathname() {
  const { pathname } = window.location;
  return pathname.startsWith('/') ? pathname.slice(1) : pathname;
}
