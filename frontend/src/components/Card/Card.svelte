<script>
  import { httpClient } from '../../http.client.js';

  import CommentBox from '../CommentBox.svelte';
  import DraggableContainer from './DraggableContainer.svelte';
  import EditableTitle from '../EditableTitle.svelte';

  export let card;

  let showTextarea = false;

  async function addComment(text) {
    if (!text.trim()) return;

    const comment = await httpClient.postComment(
      card.id,
      text,
      card.comments.length
    );
    card = { ...card, comments: [...card.comments, comment] };
  }

  function deleteComment(commentId) {
    card = {
      ...card,
      comments: card.comments.filter(({ id }) => id !== commentId),
    };
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
</script>

<style>
  .-top-2 {
    top: -2rem;
  }

  #stickyContainer {
    transition: box-shadow 0.1s ease 0s;
    background-color: white;
  }

  @supports (
    (-webkit-backdrop-filter: saturate(180%) blur(5px)) or
      (backdrop-filter: saturate(180%) blur(5px))
  ) {
    #stickyContainer {
      -webkit-backdrop-filter: saturate(180%) blur(5px);
      backdrop-filter: saturate(180%) blur(5px);
      transition: box-shadow 0.1s ease 0s;
      background-color: rgba(255, 255, 255, 0.5);
    }
  }
</style>

<div
  class="h-full flex flex-col py-8 rounded-lg bg-white border-gray-400
  align-center w-full shadow-md overflow-y-scroll scrollable-container">
  <div id="stickyContainer" class="sticky -top-2 z-20 pl-8 pr-6">
    <div class="mb-2">
      <EditableTitle title={card.title} updateTitle={onUpdateTitle}>
        <span class="font-semibold">{card.title}</span>
      </EditableTitle>
    </div>
    <CommentBox onSubmit={addComment} />
  </div>

  <div class="relative">
    <DraggableContainer
      comments={card.comments}
      {deleteComment}
      {likeComment}
      {updateCommentList} />
  </div>
</div>
