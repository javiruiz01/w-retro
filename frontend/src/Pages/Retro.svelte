<script>
  import { sessionStore } from '../Stores/SessionStore.js';
  import { cardsStore } from '../Stores/CardsStore.js';
  import { httpClient } from '../http.client.js';
  import { onMount } from 'svelte';
  import Card from '../components/Card.svelte';
  import EditableTitle from '../components/EditableTitle.svelte';
  import FabButton from '../components/FabButton.svelte';
  import Loader from '../components/Loader.svelte';

  let columns = [];
  let isLoading = true;
  let retroTitle = '';

  onMount(
    () =>
      void httpClient.fetchSession().then(({ title, cards }) => {
        isLoading = true;
        cardsStore.set(cards);
        sessionStore.update((value) => Object.assign({}, value, { title }));
      })
  );

  cardsStore.subscribe((value) => {
    columns = value;
    isLoading = false;
  });

  sessionStore.subscribe(({ title }) => void (retroTitle = title));

  $: getMargin = (idx) => (idx === columns.length - 1 ? 'mr-0' : 'mr-8');

  const onClick = () =>
    void httpClient
      .addColumn(columns.length)
      .then((res) => void (columns = [...columns, res]));

  const updateTitle = (title) => {
    retroTitle = title;
    httpClient.updateSessionTitle(title);
  };
</script>

<div class="flex flex-col h-full max-w-screen-lg mx-auto">
  {#if isLoading}
    <Loader />
  {:else}
    <div class="w-full sm:w-1/3">
      <EditableTitle title={retroTitle} {updateTitle}>
        <h2 class="text-lg text-gray-700 hover:text-teal-600 leading-snug">
          {retroTitle}
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
