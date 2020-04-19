<script>
  import Button from './Button.svelte';

  export let onButtonClick;
</script>

<div class="flex flex-col justify-center items-center mt-auto mb-auto">
  <img
    style="max-width: 400px;"
    class="w-full"
    src="assets/icons/empty.svg"
    alt="empty image" />
  <div class="flex flex-col justify-center items-center mt-6">
    <span class="text-md text-gray-700">
      Oops, we found nothing for that id
    </span>
    <div class="mt-2">
      <Button
        onClick={onButtonClick}
        override="text-gray-900 font-semibold hover:text-white bg-teal-500
        hover:bg-teal-800 p-4">
        Go back
      </Button>
    </div>
  </div>
</div>
