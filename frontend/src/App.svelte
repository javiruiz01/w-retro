<script>
  import { hubClient } from './hub.js';
  import { onMount } from 'svelte';
  import { sessionStore } from './Stores/SessionStore.js';
  import Navbar from './components/Navbar.svelte';
  import RetroPage from './Pages/Retro.svelte';
  import SessionPage from './Pages/Session.svelte';
  import Tailwind from './Tailwindcss.svelte';

  let ready = false;

  sessionStore.subscribe(({ id }) => {
    ready = !!id.trim();
    if (ready) {
      hubClient.addToGroup(id);
    }
  });

  onMount(() => void hubClient.initHubConnection());
</script>

<Navbar />
<main class="main h-full p-12">
  {#if ready}
    <RetroPage />
  {:else}
    <SessionPage />
  {/if}
</main>
