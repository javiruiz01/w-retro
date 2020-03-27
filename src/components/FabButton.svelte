<script>
  export let onClick;
</script>

<button
  on:click={onClick}
  class="absolute bottom-0 right-0 mr-5 mb-10">
  <svg
    fill="none"
    stroke="currentColor"
    stroke-linecap="round"
    stroke-linejoin="round"
    stroke-width="2"
    viewBox="0 0 24 24"
    class="w-12 h-12 stroke-current text-teal-700">
    <path d="M12 9v3m0 0v3m0-3h3m-3 0H9m12 0a9 9 0 11-18 0 9 9 0 0118 0z" />
  </svg>
</button>
