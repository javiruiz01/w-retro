<script>
  import { slide } from 'svelte/transition';
  import Comment from './Comment.svelte';

  export let comments = [];
  export let deleteComment;
  export let likeComment;

  let position = 0;
  const dataTransferType = 'text/plain';
  $: ids = comments.map(({ id }) => id);

  function onDragStart(event) {
    event.dataTransfer.setData(dataTransferType, event.target.id);
  }

  function onDragEnter(event, idx) {
    event.dataTransfer.dropEffect = 'move';

    console.log(event);
    position = idx;

    event.target.classList.add('bg-gray-400');
  }

  function onDragLeave(event) {
    event.target.classList.remove('bg-gray-400');
  }

  function onDrop(event) {
    event.target.classList.remove('bg-gray-400');

    const commentId = event.dataTransfer.getData(dataTransferType);
    const draggedElement = comments.find(({ id }) => id === commentId);
    const otherElements = comments.filter(({ id }) => id !== commentId);

    comments = [
      ...otherElements.slice(0, position),
      draggedElement,
      ...otherElements.slice(position),
    ];

    event.dataTransfer.clearData(dataTransferType);
  }
</script>

<div class="pl-8 pr-6">
  {#each comments as element, idx}
    <div
      id={element.id}
      draggable="true"
      on:dragstart={onDragStart}
      on:dragover|preventDefault={() => {}}
      on:drop|preventDefault={onDrop}
      on:dragenter|preventDefault={($event) => onDragEnter($event, idx)}
      on:dragleave|preventDefault={onDragLeave}
      transition:slide|local={{ duration: 300 }}
      class="mt-2 w-full">
      <Comment {element} {deleteComment} {likeComment} />
    </div>
  {/each}
</div>
