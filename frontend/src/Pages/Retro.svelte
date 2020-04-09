<script>
  import { cardsStore } from '../Stores/CardsStore.js';
  import { addColumn, fetchCards } from '../http.client.js';
  import { onMount } from 'svelte';
  import Card from '../components/Card.svelte';
  import FabButton from '../components/FabButton.svelte';

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
    void addColumn(columns.length).then((res) => (columns = [...columns, res]));
</script>

<style>
  .animate {
    border-top: 8px solid #38b2ac;
    animation: spin 2s linear infinite;
  }

  @keyframes spin {
    0% {
      transform: rotate(0deg);
    }
    100% {
      transform: rotate(360deg);
    }
  }
</style>

<div class="flex flex-col h-full max-w-screen-lg mx-auto">
  {#if isLoading}
    <div
      class="animate h-24 w-24 self-center m-auto border-solid border-8
      border-white rounded-full" />
  {:else}
    <h2 class="text-lg text-gray-700 mb-4">Sprint 6 retro</h2>
    <div
      class="overflow-x-scroll scrollable-container box-shadow-lat h-full pb-2
      pl-1 flex h-full">
      {#each columns as card, idx}
        <div class="w-full min-w-13 {getMargin(idx)}">
          <Card {card} />
        </div>
      {/each}
    </div>
    <FabButton {onClick} />
  {/if}
</div>
