<script>
  import { clickOutsideDirective } from '../directives/clickOutside.js';
  import SubmitIcon from './icons/Submit.svelte';
  import { tick } from 'svelte';
  import EditIcon from './icons/Edit.svelte';

  export let title = '';
  export let updateTitle;

  let inputBox;
  let isEditing = false;

  const toggleEditMode = () => {
    isEditing = true;
    tick().then(() => inputBox.focus());
  };

  const closeEdit = () => void (isEditing = false);

  const onSubmit = () => {
    if (inputBox.value.trim()) {
      updateTitle(inputBox.value);
      closeEdit();
    }
  };
</script>

{#if isEditing}
  <form
    class="relative"
    on:submit|preventDefault={onSubmit}
    use:clickOutsideDirective
    on:clickOutside={closeEdit}>
    <input
      class="mb-2 w-full border border-gray-300 rounded-lg p-2 pl-4 block
      appearance-none leading-snug shadow-sm focus:outline-none
      focus:shadow-outline"
      type="text"
      bind:this={inputBox}
      placeholder={title} />
    <button class="appearance-none absolute top-0 right-0 m-3" tabindex="0" on:click={onSubmit}>
      <SubmitIcon overrides="" />
    </button>
  </form>
{:else}
  <div
    class="w-full text-gray-700 hover:text-teal-600 mb-2 cursor-pointer flex
    items-center p-2 pr-0"
    on:click={toggleEditMode}>
    <span class="mr-1">{title}</span>
    <EditIcon />
  </div>
{/if}
