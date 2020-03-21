<script>
  import Tailwindcss from './Tailwindcss.svelte';
  import Navbar from './Navbar.svelte';
  import Card from './Card.svelte';

  const cards = [
    {
      title: 'Good',
      elements: [{ text: 'So much work!' }],
    },
    {
      title: 'Meh',
      elements: [{ text: 'Innovation week' }],
    },
    {
      title: 'Bad',
      elements: [{ text: 'Something about infra' }],
    },
  ];

  $: getMargin = (idx) => (idx === cards.length - 1 ? 'mr-0' : 'mr-8');
</script>

<Navbar />
<main class="main h-full p-12">
  <div class="flex flex-col h-full">
    <span class="text-lg text-gray-700 mb-4">Sprint 6 retro</span>
    <div class="flex h-full overflow-x-scroll scrollable-container">
      {#each cards as card, idx}
        <div class="w-full min-w-1/4 {getMargin(idx)}">
          <Card data={card} />
        </div>
      {/each}
    </div>
  </div>
</main>
