<script>
  import { cardsStore } from '../Stores/CardsStore.js';
  import { httpClient } from '../http.client.js';
  import { hubClient } from '../hub.js';
  import { onMount } from 'svelte';
  import { sessionStore, emptySession } from '../Stores/SessionStore.js';
  import Button from '../components/Button.svelte';
  import Loader from '../components/Loader.svelte';
  import RetroPage from './Retro.svelte';

  let columns = [];
  let isLoading = true;
  let doesNotExist = false;

  onMount(() => {
    isLoading = true;

    httpClient.fetchSession().then((session) => {
      if (!session) {
        doesNotExist = true;
      } else {
        notify(session);
      }
      isLoading = false;
    });
  });

  function goBack() {
    sessionStore.set(emptySession);
  }

  function notify({ id, title, cards }) {
    cardsStore.set(cards);
    sessionStore.update((value) => Object.assign({}, value, { title }));
    hubClient.addToGroup(id);
  }
</script>

<div class="flex flex-col h-full max-w-screen-lg mx-auto">
  {#if isLoading}
    <Loader />
  {:else if doesNotExist}
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
            onClick={goBack}
            override="text-gray-900 font-semibold hover:text-white bg-teal-500
            hover:bg-teal-800 p-4">
            Go back
          </Button>
        </div>
      </div>
    </div>
  {:else}
    <RetroPage />
  {/if}
</div>
