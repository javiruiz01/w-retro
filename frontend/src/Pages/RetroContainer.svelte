<script>
  import { cardsStore } from '../Stores/CardsStore.js';
  import { httpClient } from '../http.client.js';
  import { hubClient } from '../hub.js';
  import { onMount } from 'svelte';
  import { sessionStore, emptySession } from '../Stores/SessionStore.js';
  import { router } from '../router';
  import Footer from '../components/Footer.svelte';
  import Loader from '../components/Loader.svelte';
  import NotFound from '../components/NotFound.svelte';
  import RetroPage from './Retro.svelte';

  export let sessionId = '';
  let columns = [];
  let isLoading = true;
  let doesNotExist = false;

  onMount(() => {
    isLoading = true;

    httpClient.fetchSession(sessionId).then((session) => {
      if (!session) {
        doesNotExist = true;
      } else {
        notify(session);
      }

      isLoading = false;
    });
  });

  function goBack() {
    router.navigate('/');
  }

  function notify({ id, title, cards }) {
    cardsStore.set(cards);
    sessionStore.update((value) => ({ ...value, title }));
    hubClient.addToGroup(id);
  }
</script>

<div class="flex flex-col max-w-screen-lg mx-auto p-12 h-full">
  {#if isLoading}
    <Loader />
  {:else if doesNotExist}
    <NotFound onButtonClick={goBack} />
  {:else}
    <RetroPage />
  {/if}
</div>
<Footer />
