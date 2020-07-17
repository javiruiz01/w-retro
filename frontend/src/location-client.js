import { emptySession, sessionStore } from './Stores/SessionStore.js';

export const locationClient = {
  handleNavigation,
  update,
  clear,
  handleInitialValue,
};

function handleNavigation() {
  const id = getIdFromUrl();
  if (id != null && id.trim() !== '') {
    sessionStore.update((value) => ({ ...value, id }));
  } else {
    sessionStore.set(emptySession);
  }
}

function update(sessionId) {
  const urlSessionId = getIdFromUrl();

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
  const id = getIdFromUrl();
  if (id != null && id.trim() !== '') {
    sessionStore.update((value) => ({ ...value, id }));
  }
}

function getIdFromUrl() {
  return new URLSearchParams(window.location.search).get('id');
}
