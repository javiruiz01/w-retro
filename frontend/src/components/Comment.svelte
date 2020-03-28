<script>
  import RemoveIcon from './icons/RemoveIcon.svelte';
  import { selectedContext } from '../ContextMenuStore.js';
  import { onMount } from 'svelte';

  export let element;
  export let deleteComment;

  let menu;
  let menuVisible = false;

  selectedContext.subscribe(() => void (menu && toggleMenu()));

  function openContextMenu(event) {
    selectedContext.update((value) => (value = element.commentId));
    const origin = { left: event.pageX, top: event.pageY };
    setPosition(origin);
  }

  function toggleMenu(command) {
    menu.style.display = command === 'show' ? 'block' : 'none';
    menuVisible = !menuVisible;
  }

  function setPosition({ top, left }) {
    menu.style.left = `${left}px`;
    menu.style.top = `${top}px`;
    toggleMenu('show');
  }
</script>

<div
  on:contextmenu|preventDefault={openContextMenu}
  class="relative text-gray-800 py-2 px-4 rounded h-auto w-full flex
  items-center border border-solid border-gray-400">
  <span>{element.text}</span>
  <button
    on:click={() => deleteComment(element.commentId)}
    class="absolute bottom-0 right-0 -mb-2 -mr-2">
    <RemoveIcon />
  </button>
</div>

<div
  bind:this={menu}
  class="contextmenu absolute hidden p-4 bg-white shadow-md cursor-pointer rounded-lg border">
  <ul>
    <li>Remove comment</li>
  </ul>
</div>
