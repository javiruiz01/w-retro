import { writable } from 'svelte/store';

export const currentPath = writable(getPathname());

export const router = {
  onLocationChange,
  navigate,
  getPathname,
  getCleanPath,
  getParamsBasedOnRouteDefinition,
};

function onLocationChange(callback) {
  window.addEventListener('popstate', callback);
}

function navigate(rawPath) {
  const path = getCleanPath(rawPath);
  window.history.pushState({ path }, '', `${window.location.origin}${path}`);
  currentPath.set(path);
}

function getPathname() {
  const { pathname } = window.location;
  return getCleanPath(pathname);
}

function getCleanPath(path = '') {
  return path.startsWith('/') ? path : `/${path}`;
}

function getParamsBasedOnRouteDefinition(route) {
  const path = getPathname().split('/');

  return route.split('/').reduce((acc, el, idx) => {
    if (el.startsWith(':')) {
      const key = el.slice(1);
      return { ...acc, [key]: path[idx] };
    }

    return acc;
  }, {});
}
