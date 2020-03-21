<script>
  export let data;
</script>

<div
  class="h-full flex flex-col p-8 bg-white rounded-lg border-gray-400 flex
  align-center max-w-sm w-full shadow-md">
  <div class="w-full text-gray-700 mb-2">{data.title}</div>
  <div class="w-full">
    <button
      class="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4
      rounded h-auto w-full flex justify-center">
      <svg
        fill="none"
        stroke="currentColor"
        stroke-linecap="round"
        stroke-linejoin="round"
        stroke-width="2"
        viewBox="0 0 24 24"
        class="w-4 h-4">
        <path d="M12 4v16m8-8H4" />
      </svg>
    </button>
  </div>
</div>
