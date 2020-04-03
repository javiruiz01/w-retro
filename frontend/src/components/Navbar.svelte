<script>
  import { session } from '../SessionStore.js';

  const handleClick = () => void session.update((_) => '');
</script>

<nav
  on:click|preventDefault={handleClick}
  class="flex text-3xl p-3 text-gray-700 shadow-md align-text-middle bg-white">
  <h1 class="ml-5 cursor-pointer">w-retro</h1>
</nav>
