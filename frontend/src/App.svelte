<script>
  import Tailwindcss from './Tailwindcss.svelte';
  import Navbar from './components/Navbar.svelte';
  import FabButton from './components/FabButton.svelte';
  import Card from './components/Card.svelte';
  import { data } from './Store.js';

  let textareaToShow;
  let cards = [];

  $: getMargin = (idx) => (idx === cards.length - 1 ? 'mr-0' : 'mr-8');
  data.subscribe((value) => void (cards = value));

  function addColumn() {
    const newCard = {
      title: 'Something',
      comments: [{ text: 'What would go here?' }],
    };
    data.update((oldCards) => [...oldCards, newCard]);
  }
</script>

<Navbar />
<main class="main h-full p-12">
  <div class="flex flex-col h-full">
    <span class="text-lg text-gray-700 mb-4">Sprint 6 retro</span>
    <div
      class="overflow-x-scroll scrollable-container box-shadow-lat h-full pb-2
      px-2 flex h-full">
      {#each cards as card, idx}
        <div class="w-full min-w-13 {getMargin(idx)}">
          <Card {card} />
        </div>
      {/each}
    </div>
  </div>
  <FabButton onClick={addColumn} />
</main>
