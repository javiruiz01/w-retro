<script>
  import CommentBox from './CommentBox.svelte';
  import RemoveIcon from './icons/RemoveIcon.svelte';
  import { postComment, removeComment } from '../http.client.js';
  export let card;
  export let idx;

  let showTextarea = false;

  function addComment(text) {
    if (!text.trim()) return;

    card.comments = [...card.comments, { text }];
    postComment('5e7e44f44b72d83920d4458a', idx, text);
  }

  function deleteComment(commentId) {
    card.comments = card.comments.filter(
      (comment) => comment.commentId !== commentId
    );
    removeComment('5e7e44f44b72d83920d4458a', idx, commentId);
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
        class="relative mt-2 text-gray-800 py-2 px-4 rounded h-auto w-full flex
        items-center border border-solid border-gray-400">
        <span>{element.text}</span>
        <button
          on:click={() => deleteComment(element.commentId)}
          class="absolute bottom-0 right-0 -mb-2 -mr-2">
          <RemoveIcon />
        </button>
      </div>
    {/each}
  </div>
</div>
