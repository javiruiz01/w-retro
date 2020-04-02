<script>
  import RetroPage from './Pages/Retro.svelte';
  import SessionPage from './Pages/Session.svelte';
  import Navbar from './components/Navbar.svelte';
  import { session } from './SessionStore.js';

  let ready = false;

  session.subscribe((value) => void (ready = !!value.trim()));
</script>

<Navbar />
<main class="main h-full p-12">
  {#if ready}
    <RetroPage />
  {:else}
    <SessionPage />
  {/if}
</main>
