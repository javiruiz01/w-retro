<script>
  import { tick } from 'svelte';
  import { clickOutsideDirective } from '../directives/clickOutside.js';

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
    <svg fill="currentColor" viewBox="0 0 20 20" class="w-4 h-4">
      <path
        d="M17.414 2.586a2 2 0 00-2.828 0L7 10.172V13h2.828l7.586-7.586a2 2 0
        000-2.828z" />
      <path
        fill-rule="evenodd"
        d="M2 6a2 2 0 012-2h4a1 1 0 010 2H4v10h10v-4a1 1 0 112 0v4a2 2 0 01-2
        2H4a2 2 0 01-2-2V6z"
        clip-rule="evenodd" />
    </svg>
  </div>
{/if}
