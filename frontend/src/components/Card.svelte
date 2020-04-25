<script>
  import { httpClient } from '../http.client.js';

  import CommentBox from './CommentBox.svelte';
  import DraggableContainer from './DraggableContainer.svelte';
  import EditableTitle from './EditableTitle.svelte';
  import RemoveIcon from './icons/Remove.svelte';

  export let card;

  let showTextarea = false;

  async function addComment(text) {
    if (!text.trim()) return;

    const comment = await httpClient.postComment(
      card.id,
      text,
      card.comments.length
    );
    card.comments = [...card.comments, comment];
  }

  function deleteComment(commentId) {
    card.comments = card.comments.filter(({ id }) => id !== commentId);
    httpClient.removeComment(commentId);
  }

  function likeComment(comment) {
    httpClient.updateComment(comment);
  }

  function updateCommentList(orderedComments) {
    card.comments = orderedComments;
    httpClient.updateCard(card);
  }

  function onUpdateTitle(title) {
    card.title = title;
    httpClient.updateCard(card);
  }

  function removeCard() {
    httpClient.removeColumn(card.id);
  }
</script>

<style>
  #cardContainer:hover #removeCardButton {
    display: block;
  }
</style>

<div
  id="cardContainer"
  class="h-full flex flex-col py-8 rounded-lg bg-white border-gray-400
  align-center w-full shadow-md overflow-y-scroll scrollable-container">
  <div class="relative sticky bg-white -top-2 z-20 pl-8 pr-6">
    <div class="mb-2">
      <EditableTitle title={card.title} updateTitle={onUpdateTitle}>
        <span>{card.title}</span>
      </EditableTitle>
    </div>
    <CommentBox onSubmit={addComment} />
    <button
      on:click|preventDefault={removeCard}
      id="removeCardButton"
      class="text-red-500 hover:text-red-700 absolute top-0 right-0 -mt-4 mr-1
      z-20 hidden">
      <RemoveIcon />
    </button>
  </div>

  <div>
    <DraggableContainer
      comments={card.comments}
      {deleteComment}
      {likeComment}
      {updateCommentList} />
  </div>
</div>
