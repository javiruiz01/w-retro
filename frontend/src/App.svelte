<script>
  import { hubClient } from './hub.js';
  import { onMount } from 'svelte';
  import { sessionStore, emptySession } from './Stores/SessionStore.js';
  import { router } from './router';
  import Link from './components/Link.svelte';
  import Navbar from './components/Navbar.svelte';
  import RetroContainerPage from './Pages/RetroContainer.svelte';
  import SessionPage from './Pages/Session.svelte';
  import Tailwind from './Tailwindcss.svelte';
  import Route from './components/Route.svelte';

  let selectedId = '';

  onMount(
    () =>
      void hubClient.initHubConnection().then(() => {
        sessionStore.subscribe(({ id }) => {
          console.log(id);
          if (id == null || id.trim() === '') {
            selectedId = null;
            return;
          }

          selectedId = id;
          router.navigate(selectedId);
        });

        router.onLocationChange(() => {
          selectedId = router.getPathname();
        });

        const route = router.getPathname();
        sessionStore.update((value) => ({ ...value, id: route }));
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
  <Route path="/">
    <SessionPage />
  </Route>
  <Route path={selectedId}>
    <div
      class="absolute h-full bg-gray-300 bottom-0 left-0"
      style="clip-path: polygon(0 50%, 0 100%, 100% 100%);width: 70%;" />
    <RetroContainerPage sessionId={selectedId} />
    <div
      class="absolute h-full bg-gray-200 bottom-0 right-0"
      style="clip-path: polygon(147% -30%, 0 100%, 100% 100%);width:
      80%;z-index: -1" />
  </Route>
</main>
