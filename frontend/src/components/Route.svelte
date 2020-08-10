<script>
  import { onMount } from 'svelte';
  import { router, currentPath } from '../router';

  export let path;
  let currentPathname;

  $: visible = shouldRender(path, currentPathname);

  onMount(
    () => void currentPath.subscribe((value) => void (currentPathname = value))
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
