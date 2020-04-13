<script>
  import { sessionStore } from '../Stores/SessionStore.js';
  import { cardsStore } from '../Stores/CardsStore.js';
  import { httpClient } from '../http.client.js';
  import { hubClient } from '../hub.js';
  import { onMount } from 'svelte';
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
    <div>Oops, that id does not exist!</div>
  {:else}
    <RetroPage />
  {/if}
</div>
