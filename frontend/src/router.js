import { writable } from 'svelte/store';

export const router = {
  onLocationChange,
  navigate,
  getCleanPath,
  getParamsBasedOnRouteDefinition,
  currentPath: writable(getCleanPath(window.location.pathname)),
};

function onLocationChange(callback) {
  window.addEventListener('popstate', callback);
}

function navigate(rawPath) {
  const path = getCleanPath(rawPath);
  window.history.pushState({ path }, '', `${window.location.origin}${path}`);
  router.currentPath.set(path);
}

function getCleanPath(path = '') {
  return path.startsWith('/') ? path : `/${path}`;
}

function getParamsBasedOnRouteDefinition(route) {
  const path = getCleanPath(window.location.pathname).split('/');

  return route.split('/').reduce((acc, el, idx) => {
    if (el.startsWith(':')) {
      const key = el.slice(1);
      return { ...acc, [key]: path[idx] };
    }

    return acc;
  }, {});
}
