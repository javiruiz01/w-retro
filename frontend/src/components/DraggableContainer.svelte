<script>
  import DraggableItem from './DraggableItem.svelte';
  import { slide } from 'svelte/transition';

  export let card;
  export let deleteComment;
  export let likeComment;

  let draggedOverIdx;

  function onDragOver(event) {
    event.preventDefault();
    event.dataTransfer.dropEffect = 'move';
  }

  function onDrop(event) {
    event.preventDefault();

    const data = event.dataTransfer.getData('text/plain');
    const element = card.comments.find(({ id }) => data === id);
    const rest = card.comments.filter(({ id }) => data !== id);

    card.comments = [
      ...rest.slice(0, draggedOverIdx),
      element,
      ...rest.slice(draggedOverIdx),
    ];
  }
</script>

<div class="pl-8 pr-6" on:dragover={onDragOver} on:drop={onDrop}>
  {#each card.comments as element, idx}
    <div
      on:dragover={() => void (draggedOverIdx = idx)}
      transition:slide|local={{ duration: 300 }}
      class="mt-2 w-full">
      <DraggableItem {element} {deleteComment} {likeComment} />
    </div>
  {/each}
</div>
