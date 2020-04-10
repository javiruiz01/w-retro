<script>
  import { cardsStore } from '../Stores/CardsStore.js';
  import { addColumn, fetchCards } from '../http.client.js';
  import { onMount } from 'svelte';
  import Card from '../components/Card.svelte';
  import EditableTitle from '../components/EditableTitle.svelte';
  import FabButton from '../components/FabButton.svelte';
  import Loader from '../components/Loader.svelte';

  let columns = [];
  let isLoading = true;

  onMount(
    () =>
      void fetchCards().then(({ cards }) => {
        isLoading = true;
        cardsStore.set(cards);
      })
  );

  cardsStore.subscribe((value) => {
    columns = value;
    isLoading = false;
  });

  $: getMargin = (idx) => (idx === columns.length - 1 ? 'mr-0' : 'mr-8');

  const onClick = () =>
    void addColumn(columns.length).then(
      (res) => void (columns = [...columns, res])
    );

  const updateTitle = (title) => console.log('Coming soon, updating: ', title);
</script>

<div class="flex flex-col h-full max-w-screen-lg mx-auto">
  {#if isLoading}
    <Loader />
  {:else}
    <div class="w-full sm:w-1/3">
      <EditableTitle title="Sprint 6 retro" {updateTitle}>
        <h2 class="text-lg text-gray-700 hover:text-teal-600 leading-snug">
          Sprint 6 retro
        </h2>
      </EditableTitle>
    </div>
    <div
      class="mt-4 overflow-x-scroll scrollable-container box-shadow-lat h-full
      pb-2 pl-1 flex h-full">
      {#each columns as card, idx}
        <div class="w-full min-w-13 {getMargin(idx)}">
          <Card {card} />
        </div>
      {/each}
    </div>
    <FabButton {onClick} />
  {/if}
</div>
