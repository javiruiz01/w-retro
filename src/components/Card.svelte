<script>
  import AddIcon from './icons/AddIcon.svelte';
  import CloseIcon from './icons/CloseIcon.svelte';
  import SubmitIcon from './icons/SubmitIcon.svelte';
  import Button from './Button.svelte';
  import { tick } from 'svelte';
  export let card;

  let showTextArea = false;
  let textareaElement;

  function toggleTextarea() {
    showTextArea = !showTextArea;
    tick().then(() => {
      textareaElement.focus();
      textareaElement.value = '';
    });
  }

  function addComment() {
    if (!textareaElement.value.trim()) return;

    card.comments = [...card.comments, { text: textareaElement.value }];
    textareaElement.value = '';
  }

  function onKeyDown(event) {
    if (event.shiftKey) return;
    switch (event.code) {
      case 'Enter':
      case 'NumpadEnter':
        event.preventDefault();
        addComment();
        break;
      case 'Escape':
        toggleTextarea();
        break;
    }
  }
</script>

<div
  class="h-full flex flex-col py-8 pl-8 pr-6 bg-transparent-white rounded-lg
  border-gray-400 flex align-center w-full shadow-md overflow-y-scroll
  scrollable-container">
  <div class="sticky bg-white -top-2">
    <div class="w-full text-gray-700 mb-2">{card.title}</div>
    <div class="w-full mb-2">
      <Button onClick={() => toggleTextarea()}>
        <AddIcon />
      </Button>
    </div>
    <div class="relative" class:hidden={!showTextArea}>
      <textarea
        bind:this={textareaElement}
        on:keydown={onKeyDown}
        placeholder="Write something!"
        class="block resize-none border border-solid border-gray-400 rounded
        focus:outline focuse:shadow-outline w-full py-2 px-4
        scrollable-container" />
      <button
        class="absolute bottom-0 right-0 mb-1 mr-1"
        on:click={() => addComment()}>
        <SubmitIcon />
      </button>
      <button
        class="absolute top-0 right-0 mt-1 mr-1"
        on:click={() => toggleTextarea()}>
        <CloseIcon />
      </button>
    </div>
  </div>

  <div class="mt-2">
    {#each card.comments as element}
      <div
        class="mt-2 text-gray-800 py-2 px-4 rounded h-auto w-full flex
        items-center border border-solid border-gray-400">
        {element.text}
      </div>
    {/each}
  </div>
</div>
