<script>
  import { onMount } from 'svelte';
  import { router } from '../router';

  export let path;
  let currentPath;

  $: visible = shouldRender(path, currentPath);

  onMount(
    () =>
      void router.currentPath.subscribe((value) => void (currentPath = value))
  );

  function shouldRender(definedPath, current) {
    const cleanPath = router.getCleanPath(definedPath).split('/');
    const cleanCurrent = router.getCleanPath(current).split('/');

    for (let idx = 0; idx < cleanPath.length; idx++) {
      const pathElement = cleanPath[idx];
      const currentElement = cleanCurrent[idx];

      if (pathElement.startsWith(':')) {
        continue;
      }

      if (pathElement !== currentElement) {
        return false;
      }
    }

    return true;
  }
</script>

{#if visible}
  <slot />
{/if}
