<script>
  import Tailwindcss from './Tailwindcss.svelte';
  import Navbar from './Navbar.svelte';
</script>

<Navbar />
<main class="main h-full p-12">
  <div class="flex flex-col h-full">
    <span class="text-lg text-gray-700 mb-4">Sprint 6 retro</span>
    <div
      class="h-full flex flex-col p-8 bg-white rounded-lg border-gray-400 flex
      align-center max-w-sm w-full">
      <div class="w-full text-gray-700 mb-2">Good</div>
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
  </div>
</main>
