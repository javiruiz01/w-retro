<script>
  import { slide } from 'svelte/transition';
  import Comment from './Comment.svelte';

  export let comments = [];
  export let deleteComment;
  export let likeComment;

  let dragging = true;

  function onMouseDown(event) {
    // Initiate drag event
    dragging = true;

    console.log(event.target);
    const { x, y } = event.target.getBoundingClientRect();

    event.target.style.position = 'absolute';
    event.target.style.transform = `translate(${x}px, ${y}px)`;
    event.target.style.transitionDuration = '1s';
  }

  function onMouseMove(event) {}
  function onMouseUp(event) {
    dragging = false;
  }
</script>

<div class="pl-8 pr-6">
  {#each comments as element, idx}
    <div
      on:mousedown|preventDefault={onMouseDown}
      on:mousemove|preventDefault={onMouseMove}
      on:mouseup|preventDefault={onMouseUp}
      id={element.id}
      transition:slide|local={{ duration: 300 }}
      class="mt-2 w-full">
      <Comment {element} {deleteComment} {likeComment} />
    </div>
  {/each}
</div>
