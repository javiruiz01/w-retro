<script>
  import CommentBox from './CommentBox.svelte';
  import Comment from './Comment.svelte';
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
      <div class="mt-2 w-full">
        <Comment {element} {deleteComment} />
      </div>
    {/each}
  </div>
</div>
