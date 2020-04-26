<script>
  import { slide } from 'svelte/transition';
  import { tick } from 'svelte';
  import AddIcon from './icons/Add.svelte';
  import Button from './Button.svelte';
  import CloseIcon from './icons/Close.svelte';
  import SubmitIcon from './icons/Submit.svelte';

  export let onSubmit;
  let showTextarea = false;
  let textareaElement;

  function submit() {
    onSubmit(textareaElement.value);
    textareaElement.value = '';
  }

  function onKeyDown(event) {
    if (event.shiftKey) return;
    switch (event.code) {
      case 'Enter':
      case 'NumpadEnter':
        event.preventDefault();
        submit();
        break;
      case 'Escape':
        toggleTextarea();
        break;
    }
  }

  function toggleTextarea() {
    showTextarea = !showTextarea;
    tick().then(() => {
      textareaElement.value = '';
      textareaElement.focus();
    });
  }
</script>

<style>
  textarea {
    backdrop-filter: saturate(180%) blur(5px);
    background-color: rgba(255, 255, 255, 0.5);
  }
</style>

<div class="w-full mb-2">
  <Button onClick={toggleTextarea}>
    {#if showTextarea}
      <CloseIcon color={'text-gray-800'} />
    {:else}
      <AddIcon />
    {/if}
  </Button>
</div>
{#if showTextarea}
  <div transition:slide|local={{ duration: 200 }} class="relative">
    <textarea
      bind:this={textareaElement}
      on:keydown={onKeyDown}
      placeholder="Write something!"
      class="block resize-none border border-solid border-gray-400 rounded
      focus:outline-none focus:shadow-outline w-full py-2 px-4
      scrollable-container" />
    <button class="absolute bottom-0 right-0 mb-1 mr-1" on:click={submit}>
      <SubmitIcon />
    </button>
    <button class="absolute top-0 right-0 mt-1 mr-1" on:click={toggleTextarea}>
      <CloseIcon />
    </button>
  </div>
{/if}
