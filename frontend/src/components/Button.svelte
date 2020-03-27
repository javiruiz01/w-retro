<script>
  export let onClick;
</script>

<button
  on:click={onClick}
  class="bg-gray-300 hover:bg-gray-400 py-2 px-4 rounded h-auto w-full flex
  justify-center">
  <slot />
</button>
