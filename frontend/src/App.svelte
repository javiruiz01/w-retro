<script>
  import { hubClient } from './hub.js';
  import { onMount } from 'svelte';
  import { sessionStore, emptySession } from './Stores/SessionStore.js';
  import { router } from './router';
  import Navbar from './components/Navbar.svelte';
  import Router from './components/Router.svelte';
  import Tailwind from './Tailwindcss.svelte';

  onMount(() => void hubClient.initHubConnection());
</script>

<style>
  main {
    height: calc(100% - 69px);
  }
</style>

<Navbar />
<main class="h-full">
  <Router />
</main>
