<script>
  import { postComment, removeComment, updateComment } from '../http.client.js';
  import CardTitle from './CardTitle.svelte';
  import Comment from './Comment.svelte';
  import CommentBox from './CommentBox.svelte';

  export let card;

  let showTextarea = false;

  async function addComment(text) {
    if (!text.trim()) return;

    const comment = await postComment(card.id, text);
    card.comments = [...card.comments, comment];
  }

  function deleteComment(commentId) {
    card.comments = card.comments.filter(({ id }) => id !== commentId);
    removeComment(commentId);
  }

  function likeComment(comment) {
    updateComment(comment);
  }
</script>

<div
  class="h-full flex flex-col py-8 bg-transparent-white rounded-lg
  border-gray-400 flex align-center w-full shadow-md overflow-y-scroll
  scrollable-container">
  <div class="sticky bg-white -top-2 z-10 pl-8 pr-6">
    <CardTitle title={card.title} />
    <CommentBox onSubmit={addComment} />
  </div>

  <div class="mt-2 pl-8 pr-6">
    {#each card.comments as element}
      <div class="mt-2 w-full">
        <Comment {element} {deleteComment} {likeComment} />
      </div>
    {/each}
  </div>
</div>
