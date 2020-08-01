<script>
  import { hubClient } from './hub.js';
  import { onMount } from 'svelte';
  import { sessionStore, emptySession } from './Stores/SessionStore.js';
  import { router } from './router';
  import Navbar from './components/Navbar.svelte';
  import RetroContainerPage from './Pages/RetroContainer.svelte';
  import SessionPage from './Pages/Session.svelte';
  import Tailwind from './Tailwindcss.svelte';

  let ready = false;

  sessionStore.subscribe(({ id }) => void (ready = !!id.trim()));

  onMount(
    () =>
      void hubClient.initHubConnection().then(() => {
        router.handleInitialValue();
        window.addEventListener(
          'popstate',
          (_) => void router.handleNavigation()
        );
      })
  );
</script>

<style>
  main {
    height: calc(100% - 69px);
  }
</style>

<Navbar />
<main class="h-full">
  {#if ready}
    <div
      class="absolute h-full bg-gray-300 bottom-0 left-0"
      style="clip-path: polygon(0 50%, 0 100%, 100% 100%);width: 70%;" />
    <RetroContainerPage />
    <div
      class="absolute h-full bg-gray-200 bottom-0 right-0"
      style="clip-path: polygon(147% -30%, 0 100%, 100% 100%);width:
      80%;z-index: -1" />
  {:else}
    <SessionPage />
  {/if}
</main>
