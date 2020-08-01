<script>
  export let onButtonClick;
</script>

<div
  class="flex flex-col justify-center items-center mt-auto mb-auto space-y-6">
  <img
    style="max-width: 400px;"
    class="w-full"
    src="assets/icons/empty.svg"
    alt="no results found"
    loading="lazy"
    width="400"
    height="313" />
  <div class="flex flex-col justify-center items-center space-y-2">
    <span class="text-md text-gray-700">
      Oops, we found nothing for that id
    </span>
    <button
      on:click={onButtonClick}
      class=" py-2 px-4 rounded h-auto w-full flex justify-center font-semibold
      text-white bg-teal-500 hover:bg-teal-800 p-4">
      Go back
    </button>
  </div>
</div>
