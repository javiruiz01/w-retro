<script>
  import { session } from '../SessionStore';

  $: rawSession = '';

  const handleSubmit = () => void session.update((_) => rawSession);
</script>

<div
  class=" w-full h-full flex flex-col lg:flex-row justify-center items-center
  p-6">
  <form
    on:submit|preventDefault={handleSubmit}
    class="w-3/4 flex flex-col mb-8 lg:mb-0">
    <label
      class="text-3xl mb-4 text-blue-900 self-center lg:self-start"
      for="sessionId">
      Input your session Id
    </label>
    <input
      bind:value={rawSession}
      class="w-4/5 bg-white focus:outline-none focus:shadow-outline border
      border-gray-300 rounded-lg py-2 px-4 block appearance-none leading-normal
      self-center lg:self-start shadow-md text-2xl"
      type="text"
      name="sessionId"
      id="sessionId" />
  </form>
  <img
    class="w-3/4 lg:w-5/12"
    src="assets/icons/collaborators.svg"
    alt="collaborators" />
</div>
