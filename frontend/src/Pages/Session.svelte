<script>
  import { session } from '../Stores/SessionStore';
  import Button from '../components/Button.svelte';

  $: rawSession = '';

  const handleSubmit = () => void session.set(rawSession);
</script>

<div class="grid grid-cols-1 grid-rows-2 h-full items-center">
  <div class="row-span-1 col-span-1 flex justify-center w-full">
    <form
      on:submit|preventDefault={handleSubmit}
      class="w-3/4 lg:w-1/2 flex flex-col mb-8 lg:mb-0 ">
      <label
        class="text-3xl mb-4 text-blue-900 self-center lg:self-start"
        for="sessionId">
        Already created a session?
      </label>
      <div class="flex flex-col lg:flex-row justify-center items-center">
        <input
          bind:value={rawSession}
          placeholder="Input your session Id here"
          class="mb-4 lg:mb-0 w-4/5 lg:w-full bg-white focus:outline-none
          focus:shadow-outline border border-gray-300 rounded-lg p-4 block
          appearance-none leading-normal self-center lg:self-start shadow-md"
          type="text"
          name="sessionId"
          id="sessionId" />
        <div class="w-2/6 pl-4">
          <Button override="text-white bg-teal-500 hover:bg-teal-800 p-4">
            Submit
          </Button>
        </div>
      </div>
    </form>
  </div>
  <img
    style="min-width: 400px; max-width: 600px;"
    class="absolute bottom-0 right-0 m-4 w-7/12 lg:w-5/12"
    src="assets/icons/collaborators.svg"
    alt="collaborators" />
</div>
