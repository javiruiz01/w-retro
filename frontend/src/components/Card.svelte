<script>
  import CommentBox from './CommentBox.svelte';
  import { postComment } from '../http.client.js';
  export let card;
  export let idx;

  let showTextarea = false;

  function addComment(text) {
    if (!text.trim()) return;

    card.comments = [...card.comments, { text }];
    postComment('5e7df96c4b72d83920d44584', idx, text);
  }
</script>

<div
  class="h-full flex flex-col py-8 pl-8 pr-6 bg-transparent-white rounded-lg
  border-gray-400 flex align-center w-full shadow-md overflow-y-scroll
  scrollable-container">
  <div class="sticky bg-white -top-2">
    <div class="w-full text-gray-700 mb-2">{card.title}</div>
    <CommentBox onSubmit={(comment) => addComment(comment)} />
  </div>

  <div class="mt-2">
    {#each card.comments as element}
      <div
        class="mt-2 text-gray-800 py-2 px-4 rounded h-auto w-full flex
        items-center border border-solid border-gray-400">
        {element.text}
      </div>
    {/each}
  </div>
</div>
