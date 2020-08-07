<script>
  import { currentPath } from '../router';

  export let path;
  export let name = '';

  const redirectTo = () => {
    window.history.pushState(
      { path, name },
      '',
      `${window.location.origin}/${path}`
    );
    currentPath.set(path);
  };
</script>

<!-- svelte-ignore a11y-missing-attribute -->
<a on:click|preventDefault={redirectTo}>{name}</a>
