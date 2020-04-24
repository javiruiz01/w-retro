<script>
  import { slide } from 'svelte/transition';
  import { onMount } from 'svelte';
  import Comment from './Comment.svelte';

  export let element;
  export let deleteComment;
  export let likeComment;
  export let notifyDragging;

  let dragging = false;
  let commentElement;

  function onDragStart(event) {
    dragging = true;
    notifyDragging(dragging);
    event.dataTransfer.setData('text/plain', element.id);
    event.target.style.cursor = 'grabbing';
    event.dataTransfer.effectAllowed = 'all';
  }

  function onDragEnd(event) {
    dragging = false;
    notifyDragging(dragging);
    event.target.style.cursor = 'grab';
  }
</script>

<div
  draggable="true"
  bind:this={commentElement}
  on:dragstart={onDragStart}
  on:dragend={onDragEnd}
  transition:slide|local={{ duration: 300 }}
  class:opacity-25={dragging}
  class="mt-2 w-full">
  <Comment {element} {deleteComment} {likeComment} />
</div>
