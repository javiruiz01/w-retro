<script>
  import { hubClient } from './hub.js';
  import { onMount } from 'svelte';
  import { sessionStore } from './Stores/SessionStore.js';
  import Navbar from './components/Navbar.svelte';
  import RetroContainerPage from './Pages/RetroContainer.svelte';
  import SessionPage from './Pages/Session.svelte';
  import Tailwind from './Tailwindcss.svelte';

  let ready = false;

  sessionStore.subscribe(({ id }) => void (ready = !!id.trim()));

  onMount(() => void hubClient.initHubConnection());
</script>

<Navbar />
<main class="main h-full p-12">
  {#if ready}
    <RetroContainerPage />
  {:else}
    <SessionPage />
  {/if}
</main>
