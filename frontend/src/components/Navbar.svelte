<script>
  import { session } from '../Stores/SessionStore.js';

  let sessionId = '';
  let showCopiedNotification = false;

  const clearSession = () => void session.set('');
  session.subscribe((value) => void (sessionId = value));

  const copySessionId = () => {
    showCopiedNotification = true;
    navigator.clipboard
      .writeText(sessionId)
      .then(
        () => void setTimeout(() => void (showCopiedNotification = false), 450)
      );
  };
</script>

<style>
  .slide-down {
    animation: slide-bottom 0.2s cubic-bezier(0.4, 0, 0.2, 1) both;
  }

  @keyframes slide-bottom {
    0% {
      transform: translateY(0);
    }
    100% {
      transform: translateY(30px);
    }
  }
</style>

<nav class="p-3 shadow-md align-text-middle bg-white">
  <div
    class="text-3xl text-gray-700 flex items-baseline flex-row justify-between
    w-full">
    <h1
      tabindex="0"
      on:click|preventDefault={clearSession}
      class="block ml-5 cursor-pointer">
      w-retro
    </h1>
    <span
      class="relative block mr-5 text-lg"
      on:click|preventDefault={copySessionId}
      class:hidden={!sessionId.trim()}>
      <span class="text-teal-500 cursor-pointer lg:cursor-default">
        Session Id
      </span>
      <span
        class="hover:bg-teal-700 hover:text-white p-1 rounded-lg cursor-pointer
        hidden md:inline-block">
        {sessionId}
      </span>
      <div
        class="absolute left-0 mt-2 bottom-auto bg-gray-700 text-white
        font-semibold slide-down hover:bg-gray-700 shadow-lg p-2 rounded-lg"
        class:hidden={!showCopiedNotification}>
        Copied!
      </div>
    </span>
  </div>
</nav>
