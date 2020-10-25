<script>
  import { httpClient } from '../../http.client.js';
  import { contextStore } from '../../Stores/ContextMenuStore.js';

  import CommentBox from '../CommentBox.svelte';
  import DraggableContainer from './DraggableContainer.svelte';
  import EditableTitle from '../EditableTitle.svelte';
  import OptionsIcon from '../icons/Options.svelte';

  export let card;

  let menu;
  let optionsButton;

  contextStore.subscribe(() => void (menu && toggleMenu(false)));

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

  function removeCard() {
    httpClient.removeColumn(card.id);
    toggleMenu(false);
  }

  function openOptions() {
    contextStore.set(card.id);
    const { x, y } = optionsButton.getBoundingClientRect();
    menu.style.left = `${x}px`;
    menu.style.top = `${y}px`;
    toggleMenu(true);
  }

  function sortCommentsByLikes() {
    card = {
      ...card,
      comments: card.comments.sort(({ likes: a }, { likes: b }) =>
        a < b ? 1 : -1
      ),
    };
    toggleMenu(false);
  }

  function toggleMenu(show) {
    menu.style.display = show ? 'block' : 'none';
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

  <button
    bind:this={optionsButton}
    on:click|preventDefault={openOptions}
    class="options absolute top-0 right-0 mt-2 mr-1 z-10 w-4 h-4">
    <OptionsIcon />
  </button>
</div>

<div
  bind:this={menu}
  class="contextmenu bg-white fixed hidden shadow-lg cursor-pointer rounded-tr-lg rounded-b-lg border-2 z-30">
  <ul tabindex="0">
    <li
      on:click|preventDefault={sortCommentsByLikes}
      tabindex="0"
      class="p-4 rounded-tr-md hover:text-white bg-white hover:bg-gray-600 foucs:bg-green-600 focus:text-white">
      Sort comments by likes
    </li>
    <li
      on:click|preventDefault={removeCard}
      tabindex="0"
      class="p-4 rounded-b-md hover:text-white bg-white hover:bg-red-500 foucs:bg-green-600 focus:text-white">
      Remove column
    </li>
  </ul>
</div>
