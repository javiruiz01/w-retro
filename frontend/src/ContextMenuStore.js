import { writable } from 'svelte/store';

export const selectedContext = writable('');

window.addEventListener('click', (event) => {
  if (!clickInsideContextMenu(event)) {
    selectedContext.update((value) => void (value = ''));
  }
});

function clickInsideContextMenu({ srcElement, target }) {
  let element = srcElement || target;
  const isContextMenu = ({ classList }) => classList.contains('contextmenu');

  if (isContextMenu(element)) {
    return true;
  }

  while ((element = element.parentNode)) {
    if (element.classList && isContextMenu(element)) {
      return true;
    }
  }

  return false;
}
