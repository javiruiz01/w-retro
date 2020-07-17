<script>
  import { hubClient } from './hub.js';
  import { onMount } from 'svelte';
  import { sessionStore, emptySession } from './Stores/SessionStore.js';
  import Navbar from './components/Navbar.svelte';
  import RetroContainerPage from './Pages/RetroContainer.svelte';
  import SessionPage from './Pages/Session.svelte';
  import Tailwind from './Tailwindcss.svelte';

  let ready = false;

  sessionStore.subscribe(({ id }) => void (ready = !!id.trim()));

  onMount(() => {
    hubClient.initHubConnection();

    const id = new URLSearchParams(window.location.search).get('id');
    if (id != null && id.trim() !== '') {
      sessionStore.update((value) => ({ ...value, id }));
    }

    window.addEventListener('popstate', (_) => {
      const id = new URLSearchParams(window.location.search).get('id');
      if (id != null && id.trim() !== '') {
        sessionStore.update((value) => ({ ...value, id }));
      } else {
        sessionStore.set(emptySession);
      }
    });
  });
</script>

<style>
  .main::before,
  .main::after {
    position: absolute;
    content: '';
    height: 100%;
  }

  .main::before {
    background-color: #e2e8f0;
    clip-path: polygon(0 50%, 0 100%, 100% 100%);
    width: 70%;
    bottom: 0;
    left: 0;
  }

  .main::after {
    background-color: #edf2f7;
    clip-path: polygon(147% -30%, 0 100%, 100% 100%);
    width: 80%;
    bottom: 0;
    right: 0;
    z-index: -1;
  }
</style>

<Navbar />
<main class="main h-full">
  {#if ready}
    <RetroContainerPage />
  {:else}
    <SessionPage />
  {/if}
</main>
