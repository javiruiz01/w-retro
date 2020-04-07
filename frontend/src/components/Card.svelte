<script>
  import {
    postComment,
    removeColumn,
    removeComment,
    updateComment,
    updateTitle,
  } from '../http.client.js';
  import { slide } from 'svelte/transition';
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

  function onUpdateTitle(title) {
    card.title = title;
    updateTitle(card.id, title);
  }

  function removeCard() {
    removeColumn(card.id);
  }
</script>

<style>
  #cardContainer:hover > #removeCardButton {
    display: block;
  }
</style>

<div
  id="cardContainer"
  class="relative h-full flex flex-col py-8 bg-transparent-white rounded-lg
  border-gray-400 flex align-center w-full shadow-md overflow-y-scroll
  scrollable-container">
  <div class="sticky bg-white -top-2 z-20 pl-8 pr-6">
    <CardTitle title={card.title} updateTitle={onUpdateTitle} />
    <CommentBox onSubmit={addComment} />
  </div>

  <div class="mt-2 pl-8 pr-6">
    {#each card.comments as element}
      <div transition:slide|local={{ duration: 300 }} class="mt-2 w-full">
        <Comment {element} {deleteComment} {likeComment} />
      </div>
    {/each}
  </div>

  <button
    on:click|preventDefault={removeCard}
    id="removeCardButton"
    class="text-red-500 hover:text-red-700 absolute top-0 right-0 mt-2 mr-1 z-20
    hidden">
    <svg fill="currentColor" viewBox="0 0 20 20" class="w-4 h-4 ">
      <path
        fill-rule="evenodd"
        d="M9 2a1 1 0 00-.894.553L7.382 4H4a1 1 0 000 2v10a2 2 0 002 2h8a2 2 0
        002-2V6a1 1 0 100-2h-3.382l-.724-1.447A1 1 0 0011 2H9zM7 8a1 1 0 012
        0v6a1 1 0 11-2 0V8zm5-1a1 1 0 00-1 1v6a1 1 0 102 0V8a1 1 0 00-1-1z"
        clip-rule="evenodd" />
    </svg>
  </button>
</div>
