<script>
  import { slide } from 'svelte/transition';
  import { onMount } from 'svelte';
  import Comment from './Comment.svelte';

  export let element;
  export let deleteComment;
  export let likeComment;

  let commentElement;

  function onDragStart(event) {
    event.dataTransfer.setData('text/plain', element.id);
    event.dataTransfer.dropEffect = 'move';
    commentElement.style.backgroundColor = 'red';
  }

  function onDragEnd(event) {
    commentElement.style.backgroundColor = 'white';
  }
</script>

<div
  draggable="true"
  bind:this={commentElement}
  on:dragstart={onDragStart}
  on:dragend={onDragEnd}
  transition:slide|local={{ duration: 300 }}
  class="mt-2 w-full">
  <Comment {element} {deleteComment} {likeComment} />
</div>
