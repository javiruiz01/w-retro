<script>
  import { clickOutsideDirective } from '../directives/clickOutside.js';
  import { tick } from 'svelte';
  import EditIcon from './icons/Edit.svelte';

  export let title = '';

  let inputBox;
  let isEditing = false;

  const toggleEditMode = () => {
    isEditing = true;
    tick().then(() => inputBox.focus());
  };

  const closeEdit = () => void (isEditing = false);
</script>

{#if isEditing}
  <input
    class="mb-2 w-full border border-gray-300 rounded-lg p-2 pl-4 block
    appearance-none leading-snug shadow-sm focus:outline-none
    focus:shadow-outline"
    type="text"
    bind:this={inputBox}
    use:clickOutsideDirective
    on:clickOutside={closeEdit}
    placeholder={title} />
{:else}
  <div
    class="w-full text-gray-700 hover:text-teal-600 mb-2 cursor-pointer flex
    items-center p-2"
    on:click={toggleEditMode}>
    <span class="mr-1">{title}</span>
    <EditIcon />
  </div>
{/if}
