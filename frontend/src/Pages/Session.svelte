<script>
  import { httpClient } from '../http.client.js';
  import { sessionStore } from '../Stores/SessionStore.js';
  import Button from '../components/Button.svelte';

  $: rawSession = '';

  const handleSubmit = () =>
    void sessionStore.update((value) =>
      Object.assign({}, value, { id: rawSession })
    );

  const createNewSession = async () =>
    void sessionStore.set({ id: await httpClient.createSession() });
</script>

<style>
  @media only screen and (max-width: 1024px) {
    #footer-image {
      bottom: 0;
      left: 1%;
      right: 1%;
      margin: auto;
      margin-bottom: 1rem;
    }
  }

  @media screen and (max-height: 750px) {
    #footer-image {
      visibility: hidden;
    }

    #session-container {
      grid-template-rows: auto;
    }
  }
</style>

<div
  id="session-container"
  class="grid grid-cols-1 grid-rows-2 h-full items-center">
  <div class="row-span-1 col-span-1 flex justify-center w-full">
    <form
      on:submit|preventDefault={handleSubmit}
      class="w-3/4 lg:w-1/2 flex flex-col mb-8 lg:mb-0 ">
      <label
        class="text-3xl mb-4 text-blue-900 text-center lg:text-left"
        for="sessionId">
        Already created a session?
      </label>
      <div class="flex flex-col lg:flex-row justify-center items-center mb-4">
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
          <Button
            override="text-gray-900 font-semibold hover:text-white bg-teal-500
            hover:bg-teal-800 p-4">
            Submit
          </Button>
        </div>
      </div>
      <span class="self-center lg:self-start">
        Or, create a new session
        <a
          class="text-teal-500 hover:text-teal-800 font-semibold "
          on:click|preventDefault={createNewSession}
          href="/">
          here
        </a>
      </span>
    </form>
  </div>
  <img
    id="footer-image"
    class="w-full absolute lg:bottom-0 lg:right-0 lg:m-4"
    style="min-width: 400px; max-width: 500px;"
    src="assets/icons/collaborators.svg"
    alt="collaborators" />
</div>
