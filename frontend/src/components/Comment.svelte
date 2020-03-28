<script>
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
  <button class="absolute bottom-0 right-0 -m-2">
    <svg
      fill="currentColor"
      viewBox="0 0 20 20"
      class="w-6 h-6 stroke-current text-green-500 hover:text-green-600
      cursor-pointer">
      <path
        fill-rule="evenodd"
        d="M3.172 5.172a4 4 0 015.656 0L10 6.343l1.172-1.171a4 4 0 115.656
        5.656L10 17.657l-6.828-6.829a4 4 0 010-5.656z"
        clip-rule="evenodd" />
    </svg>
  </button>
</div>

<div
  bind:this={menu}
  on:click={() => deleteComment(element.commentId)}
  class="contextmenu absolute hidden shadow-lg cursor-pointer rounded-tr-lg
  rounded-b-lg border-2 z-10 ">
  <ul>
    <li class=" p-4 rounded-tr-md hover:text-white bg-white hover:bg-green-600">
      Like comment
    </li>
    <li class=" p-4 rounded-b-md hover:text-white bg-white hover:bg-red-500">
      Remove comment
    </li>
  </ul>
</div>
