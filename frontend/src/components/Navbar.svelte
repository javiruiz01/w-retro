<script>
  import { sessionStore, emptySession } from '../Stores/SessionStore.js';
  import { cardsStore } from '../Stores/CardsStore.js';

  let sessionId = '';
  let showCopiedNotification = false;

  const clearSession = () => {
    cardsStore.set([]);
    sessionStore.set(emptySession);
  };

  sessionStore.subscribe(({ id }) => void (sessionId = id));

  const removeNotification = () =>
    void setTimeout(() => void (showCopiedNotification = false), 450);

  const copySessionId = () => {
    showCopiedNotification = true;
    if (navigator.clipboard) {
      navigator.clipboard.writeText(sessionId).then(removeNotification);
    } else {
      const listener = (e) => {
        e.clipboardData.setData('text/plain', sessionId);
        e.preventDefault();
      };
      document.addEventListener('copy', listener, false);
      document.execCommand('copy');
      document.removeEventListener('copy', listener, false);
      removeNotification();
    }
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

<header
  class="p-3 shadow-md align-text-middle bg-white flex items-baseline flex-row
  justify-between">
  <aside class="text-3xl text-gray-700 cursor-pointer">
    <h1 tabindex="0" on:click|preventDefault={clearSession} class="block ml-5 ">
      w-retro
    </h1>
  </aside>
  <aside>
    <span
      class="relative block mr-5 text-lg"
      on:click|preventDefault={copySessionId}
      class:hidden={!sessionId.trim()}>
      <span class="text-teal-500 lg:cursor-default">Session Id</span>
      <span
        class="hover:bg-teal-700 hover:text-white p-1 rounded-lg hidden
        md:inline-block cursor-pointer">
        {sessionId}
      </span>
      <div
        class="absolute right-0 mt-2 bottom-auto bg-gray-700 text-white
        font-semibold slide-down hover:bg-gray-700 shadow-lg p-2 rounded-lg"
        class:hidden={!showCopiedNotification}>
        Copied!
      </div>
    </span>
  </aside>
</header>
