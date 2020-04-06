<script>
  import { initHubConnection, addToGroup } from './Hub.js';
  import { onMount } from 'svelte';
  import { sessionStore } from './Stores/SessionStore.js';
  import Navbar from './components/Navbar.svelte';
  import RetroPage from './Pages/Retro.svelte';
  import SessionPage from './Pages/Session.svelte';
  import Tailwind from './Tailwindcss.svelte';

  let ready = false;

  sessionStore.subscribe((value) => {
    ready = !!value.trim();
    if (ready) {
      addToGroup(value);
    }
  });

  onMount(() => void initHubConnection());
</script>

<Navbar />
<main class="main h-full p-12">
  {#if ready}
    <RetroPage />
  {:else}
    <SessionPage />
  {/if}
</main>
