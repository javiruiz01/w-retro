<script>
  import { onMount } from 'svelte';
  import { cardsStore } from '../Stores/CardsStore.js';
  import { httpClient } from '../http.client.js';
  import { sessionStore } from '../Stores/SessionStore.js';
  import Card from '../components/Card/Card.svelte';
  import EditableTitle from '../components/EditableTitle.svelte';
  import EmptyState from '../components/EmptyState.svelte';
  import FabButton from '../components/FabButton.svelte';
  import RemoveIcon from '../components/icons/Remove.svelte';

  let columns = [];
  let retroTitle = '';
  let observer;
  let showRightShadow = false;
  let showLeftShadow = false;

  cardsStore.subscribe((value) => void (columns = value));
  sessionStore.subscribe(({ title }) => void (retroTitle = title));

  onMount(() => {
    const options = {
      root: document.querySelector('.intersection'),
      threshold: 1.0,
    };

    observer = new IntersectionObserver(
      (entries) =>
        void entries.forEach((entry) => {
          switch (entry.target.id) {
            case 'begin':
              showLeftShadow = !entry.isIntersecting;
              break;
            case 'end':
              showRightShadow = !entry.isIntersecting;
              break;
          }
        }),
      options
    );

    observer.observe(document.querySelector('#begin'));
    observer.observe(document.querySelector('#end'));
  });

  const addCard = () =>
    void httpClient
      .addColumn(columns.length)
      .then((res) => void (columns = [...columns, res]));

  const updateTitle = (title) => {
    retroTitle = title;
    httpClient.updateSessionTitle(title);
  };

  const removeCard = (card) => void httpClient.removeColumn(card.id);
</script>

<style>
  .left-shadow::before,
  .right-shadow::after {
    height: calc(100% - 0.75rem);
    width: 8px;
    z-index: 30;
    content: '';
    position: absolute;
    pointer-events: none;
    top: 0;
  }

  .left-shadow::before {
    left: 0px;
    background: linear-gradient(
      to left,
      rgba(99, 114, 130, 0) 0px,
      rgba(9, 30, 66, 0.13) 100%
    );
    border-top-right-radius: 4px;
    border-bottom-right-radius: 4px;
  }

  .right-shadow::after {
    left: calc(100% - 8px);
    background: linear-gradient(
      to right,
      rgba(99, 114, 130, 0) 0px,
      rgba(9, 30, 66, 0.13) 100%
    );
    border-top-left-radius: 4px;
    border-bottom-left-radius: 4px;
  }

  #cardContainer:hover button#removeCardButton {
    display: block;
  }
</style>

<div class="h-full flex flex-col">
  <div class="w-full sm:w-1/3">
    <EditableTitle title={retroTitle} {updateTitle}>
      <h2
        class="text-lg text-gray-700 hover:text-teal-600 leading-snug font-bold">
        {retroTitle}
      </h2>
    </EditableTitle>
  </div>
  {#if columns.length > 0}
    <div
      class="intersection relative w-full h-full flex-1 min-h-0 mt-4"
      class:left-shadow={showLeftShadow}
      class:right-shadow={showRightShadow}>
      <div class="overflow-x-scroll scrollable-container h-full">
        <div class="pb-2 px-1 flex h-full">
          <div id="begin" />
          {#each columns as card, idx}
            <div class="relative w-full h-full min-w-15" id="cardContainer">
              <button
                on:click|preventDefault={() => removeCard(card)}
                id="removeCardButton"
                class="text-red-500 hover:text-red-700 absolute top-0 right-0
                mr-2 mt-4 z-30 hidden">
                <RemoveIcon />
              </button>
              <Card {card} />

            </div>
            {#if idx !== columns.length - 1}
              <div class="px-4" />
            {:else}
              <div class="pr-1" />
            {/if}
          {/each}
          <div id="end" />
        </div>
      </div>
    </div>
  {:else}
    <EmptyState />
  {/if}
</div>

<FabButton onClick={addCard} />
