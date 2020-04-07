<script>
  import { cardsStore } from '../Stores/CardsStore.js';
  import { addColumn, fetchCards } from '../http.client.js';
  import { onMount } from 'svelte';
  import Card from '../components/Card.svelte';
  import FabButton from '../components/FabButton.svelte';

  let columns = [];

  onMount(
    () => void fetchCards().then(({ cards }) => void cardsStore.set(cards))
  );

  cardsStore.subscribe((value) => void (columns = value));

  $: getMargin = (idx) => (idx === columns.length - 1 ? 'mr-0' : 'mr-8');

  const onClick = () =>
    void addColumn(columns.length).then((res) => (columns = [...columns, res]));
</script>

<div class="flex flex-col h-full max-w-screen-lg mx-auto">
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
</div>
<FabButton {onClick} />
