<script>
  import { currentPath } from '../router';
  import { onMount } from 'svelte';
  import Route from './Route.svelte';
  import SessionPage from '../pages/Session.svelte';
  import RetroContainerPage from '../pages/RetroContainer.svelte';

  onMount(() => {
    currentPath.subscribe(console.log);
  });
</script>

<Route path="/">
  <SessionPage />
</Route>

<Route path="/session/:id">
  <div
    class="absolute h-full bg-gray-300 bottom-0 left-0"
    style="clip-path: polygon(0 50%, 0 100%, 100% 100%);width: 70%;" />
  <RetroContainerPage />
  <div
    class="absolute h-full bg-gray-200 bottom-0 right-0"
    style="clip-path: polygon(147% -30%, 0 100%, 100% 100%);width: 80%;z-index:
    -1" />
</Route>
