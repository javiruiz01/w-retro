<script>
  import { slide } from 'svelte/transition';
  import DraggableItem from './DraggableItem.svelte';

  export let comments;
  export let deleteComment;
  export let likeComment;

  let draggedOverIdx;
  let isAvailableDropzone = false;

  function onDragOver(event) {
    if (isAvailableDropzone) {
      event.preventDefault();
    }
  }

  function onDrop(event) {
    const data = event.dataTransfer.getData('text/plain');
    const element = comments.find(({ id }) => data === id);

    if (!element) {
      return;
    }
    event.preventDefault();

    const rest = comments.filter(({ id }) => data !== id);
    comments = [
      ...rest.slice(0, draggedOverIdx),
      element,
      ...rest.slice(draggedOverIdx),
    ];
  }
</script>

<div class="pl-8 pr-6 h-full" on:dragover={onDragOver} on:drop={onDrop}>
  {#each comments as element, idx}
    <div
      on:dragover={() => void (draggedOverIdx = idx)}
      transition:slide|local={{ duration: 300 }}
      class="w-full">
      <DraggableItem
        {element}
        {deleteComment}
        {likeComment}
        notifyDragging={(event) => (isAvailableDropzone = event)} />
    </div>
  {/each}
</div>
