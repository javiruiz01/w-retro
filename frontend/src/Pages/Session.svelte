<script>
  import Button from '../components/Button.svelte';
  import { httpClient } from '../http.client.js';
  import { sessionStore } from '../Stores/SessionStore.js';

  $: rawSession = '';

  const handleSubmit = () =>
    void sessionStore.update((value) => ({ ...value, id: rawSession }));

  const createNewSession = async () =>
    void sessionStore.set({ id: await httpClient.createSession() });
</script>

<div class="flex max-w-md h-full items-center mx-auto">
  <div class="bg-white rounded-lg shadow-md w-full px-8 py-10">
    <form
      class="flex flex-col space-y-8"
      on:submit|preventDefault={handleSubmit}>
      <label for="sessionId" class="text-2xl text-blue-900">
        Already started a retro session?
      </label>
      <img
        class="w-full block"
        src="assets/icons/collaborators.svg"
        alt="collaborators"
        loading="lazy"
        width="384"
        height="290" />
      <div class="flex flex-col space-y-4">
        <input
          bind:value={rawSession}
          placeholder="Input your session Id here"
          class="bg-white focus:outline-none focus:shadow-outline border
          border-gray-300 rounded-md p-4 block appearance-none leading-normal
          shadow-sm"
          type="text"
          name="sessionId"
          id="sessionId" />
        <div class="flex space-x-2">
          <button
            on:click={createNewSession}
            class="hover:bg-gray-400 py-2 px-4 rounded-md h-auto w-full flex
            items-center justify-center border border-solid border-gray-400
            focus:outline-none focus:shadow-outline">
            Create a new session
          </button>
          <button
            class="py-2 px-4 rounded-md h-auto w-full flex justify-center
            items-center font-semibold text-white bg-teal-500 hover:bg-teal-800
            focus:outline-none focus:shadow-outline">
            Submit
          </button>
        </div>
      </div>
    </form>
  </div>
</div>
