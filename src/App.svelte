<script>
  import Tailwindcss from './Tailwindcss.svelte';
  import Navbar from './Navbar.svelte';

  let commentText = '';
  let textareaToShow;
  const cards = [
    {
      title: 'Good',
      comments: [{ text: 'So much work!' }],
    },
    {
      title: 'Meh',
      comments: [{ text: 'Innovation week canceled' }],
    },
    {
      title: 'Bad',
      comments: [{ text: 'Something about infra' }],
    },
  ];

  $: getMargin = (idx) => (idx === cards.length - 1 ? 'mr-0' : 'mr-8');

  function toggleTextarea(idx) {
    textareaToShow = textareaToShow === idx ? '' : idx;
  }

  function addComment(idx) {
    if (!commentText.trim()) return;

    const comments = cards[idx].comments;
    cards[idx].comments = [...comments, { text: commentText }];
    commentText = '';
  }
</script>

<Navbar />
<main class="main h-full p-12">
  <div class="flex flex-col h-full">
    <span class="text-lg text-gray-700 mb-4">Sprint 6 retro</span>
    <div class="flex h-full overflow-x-scroll scrollable-container">
      {#each cards as { title, comments }, idx}
        <div class="w-full min-w-13 {getMargin(idx)}">
          <div
            class="h-full flex flex-col py-8 pl-8 pr-6 bg-white rounded-lg
            border-gray-400 flex align-center w-full shadow-md overflow-y-scroll
            scrollable-container">
            <div class="sticky bg-white -top-2">
              <div class="w-full text-gray-700 mb-2">{title}</div>
              <div class="w-full mb-2">
                <button
                  on:click={() => toggleTextarea(idx)}
                  class="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold
                  py-2 px-4 rounded h-auto w-full flex justify-center">
                  <svg
                    fill="none"
                    stroke="currentColor"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    viewBox="0 0 24 24"
                    class="w-4 h-4">
                    <path d="M12 4v16m8-8H4" />
                  </svg>
                </button>
              </div>
              <div class="relative" class:hidden={textareaToShow !== idx}>
                <textarea
                  bind:value={commentText}
                  placeholder="Write something!"
                  class="block resize-none border border-solid border-gray-400
                  rounded focus:outline focuse:shadow-outline w-full py-2 px-4
                  scrollable-container" />
                <button
                  class="absolute top-0 right-0 mt-1 mr-1"
                  on:click={() => toggleTextarea(idx)}>
                  <svg
                    fill="none"
                    stroke="currentColor"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    viewBox="0 0 24 24"
                    class="w-4 h-4 stroke-current text-red-500 cursor-pointer">
                    <path d="M6 18L18 6M6 6l12 12" />
                  </svg>
                </button>
                <button
                  class="absolute bottom-0 right-0 mb-1 mr-1"
                  on:click={() => addComment(idx)}>
                  <svg
                    fill="none"
                    stroke="currentColor"
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    viewBox="0 0 24 24"
                    class=" w-4 h-4 stroke-current text-green-500 cursor-pointer">
                    <path
                      d="M13 9l3 3m0 0l-3 3m3-3H8m13 0a9 9 0 11-18 0 9 9 0 0118
                      0z" />
                  </svg>
                </button>
              </div>
            </div>

            <div class="mt-2">
              {#each comments as element}
                <div
                  class="mt-2 text-gray-800 py-2 px-4 rounded h-auto w-full flex
                  items-center border border-solid border-gray-400">
                  {element.text}
                </div>
              {/each}
            </div>
          </div>
        </div>
      {/each}
    </div>
  </div>
</main>
