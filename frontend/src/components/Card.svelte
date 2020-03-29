<script>
  import CommentBox from './CommentBox.svelte';
  import Comment from './Comment.svelte';
  import { postComment, removeComment, updateComment } from '../http.client.js';
  export let card;
  export let idx;

  let showTextarea = false;

  async function addComment(text) {
    if (!text.trim()) return;

    const comment = await postComment(idx, text);
    card.comments = [...card.comments, comment];
  }

  function deleteComment(commentId) {
    card.comments = card.comments.filter(
      (comment) => comment.commentId !== commentId
    );
    removeComment(idx, commentId);
  }

  function likeComment(commentIdx, comment) {
    updateComment(idx, commentIdx, comment);
  }
</script>

<div
  class="h-full flex flex-col py-8 bg-transparent-white rounded-lg
  border-gray-400 flex align-center w-full shadow-md overflow-y-scroll
  scrollable-container">
  <div class="sticky bg-white -top-2 z-10 pl-8 pr-6">
    <div class="w-full text-gray-700 mb-2">{card.title}</div>
    <CommentBox onSubmit={(comment) => addComment(comment)} />
  </div>

  <div class="mt-2 pl-8 pr-6">
    {#each card.comments as element, idx}
      <div class="mt-2 w-full">
        <Comment
          {element}
          {deleteComment}
          likeComment={(comment) => likeComment(idx, comment)} />
      </div>
    {/each}
  </div>
</div>
