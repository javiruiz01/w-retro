<script>
  import { onMount } from 'svelte';
  import { router, currentPath } from '../router';

  export let path = '';
  let currentPathname = '';

  $: visible = normalizedPath(path) === normalizedPath(currentPathname);

  onMount(
    () => void currentPath.subscribe((value) => void (currentPathname = value))
  );

  function normalizedPath(value) {
    return value == null || value.startsWith('/') ? value : `/${value}`;
  }
</script>

{#if visible}
  <slot />
{/if}
