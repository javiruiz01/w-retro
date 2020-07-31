import { emptySession, sessionStore } from './Stores/SessionStore.js';

export const router = {
  handleNavigation,
  update,
  clear,
  handleInitialValue,
};

function handleNavigation() {
  const id = getFromQueryParams('id');
  if (id != null && id.trim() !== '') {
    sessionStore.update((value) => ({ ...value, id }));
  } else {
    sessionStore.set(emptySession);
  }
}

function update(sessionId) {
  const urlSessionId = getFromQueryParams('id');

  if (urlSessionId !== sessionId) {
    const queryParams = new URLSearchParams({
      id: sessionId,
    }).toString();
    window.history.pushState(null, null, `?${queryParams}`);
  }
}

function clear() {
  const [url] = window.location.href.split('?');
  window.history.pushState(null, null, url);
}

function handleInitialValue() {
  const id = getFromQueryParams('id');
  if (id != null && id.trim() !== '') {
    sessionStore.update((value) => ({ ...value, id }));
  }
}

function getFromQueryParams(key) {
  return new URLSearchParams(window.location.search).get(key);
}
