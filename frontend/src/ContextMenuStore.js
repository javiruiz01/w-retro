import { writable } from 'svelte/store';

const contextMenuClass = 'contextmenu';
const optionsClass = 'options';

export const selectedContext = writable('');

window.addEventListener('click', (event) => {
  if (
    !shouldKeepMenuOpen(event, contextMenuClass) &&
    !shouldKeepMenuOpen(event, optionsClass)
  ) {
    selectedContext.update((value) => void (value = ''));
  }
});

function shouldKeepMenuOpen({ srcElement, target }, className) {
  let element = srcElement || target;
  const isContextMenu = ({ classList }) =>
    classList && classList.contains(className);

  if (isContextMenu(element)) {
    return true;
  }

  while ((element = element.parentNode)) {
    if (isContextMenu(element)) {
      return true;
    }
  }

  return false;
}
