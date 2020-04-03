<script>
  import { selectedContext } from '../Stores/ContextMenuStore.js';
  import { onMount } from 'svelte';
  import OptionsIcon from './icons/OptionsIcon.svelte';
  import LikeIcon from './icons/LikeIcon.svelte';

  export let element;
  export let deleteComment;
  export let likeComment;

  let menu;
  let optionsButton;
  let menuVisible = false;

  selectedContext.subscribe(() => void (menu && toggleMenu()));

  function onDeleteComment() {
    deleteComment(element.id);
    toggleMenu();
  }

  function onLikeComment() {
    element.likes += 1;
    likeComment(element);
    selectedContext.set('');
  }

  function openByOptions() {
    selectedContext.set(element.id);
    const { x: left, y: top } = optionsButton.getBoundingClientRect();
    setPosition({ left, top });
  }

  function openContextMenu(event) {
    selectedContext.set(element.id);
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
  class="relative text-gray-800 rounded h-auto w-full flex items-center border
  border-solid border-gray-400">
  <div
    class="py-4 pl-4 pr-8 w-full"
    on:contextmenu|preventDefault={openContextMenu}>
    <span>{element.text}</span>
  </div>
  <div class="absolute bottom-0 right-0 mb-1 mx-2 z-10">
    <button
      on:click={onLikeComment}
      class="flex flex-row justify-center flex flex-row justify-center
      items-center text-green-500 hover:text-green-600 cursor-pointer">
      <span class:hidden={element.likes === 0} class="font-semibold mr-1">
        +{element.likes}
      </span>
      <LikeIcon />
    </button>
  </div>
  <button
    bind:this={optionsButton}
    on:click|preventDefault={openByOptions}
    class="options absolute top-0 right-0 mt-2 mr-1 z-10">
    <OptionsIcon />
  </button>
</div>

<div
  bind:this={menu}
  class="contextmenu absolute hidden shadow-lg cursor-pointer rounded-tr-lg
  rounded-b-lg border-2 z-20">
  <ul tabindex="0">
    <li
      on:click={onLikeComment}
      tabindex="0"
      class=" p-4 rounded-tr-md hover:text-white bg-white hover:bg-green-600
      focus:bg-green-600 focus:text-white ">
      Like comment
    </li>
    <li
      tabindex="0"
      on:click={onDeleteComment}
      class=" p-4 rounded-b-md hover:text-white bg-white hover:bg-red-500
      focus:bg-red-500 focus:text-white ">
      Remove comment
    </li>
  </ul>
</div>
