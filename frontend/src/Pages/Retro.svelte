<script>
  import { cardsStore } from '../Stores/CardsStore.js';
  import { httpClient } from '../http.client.js';
  import { sessionStore } from '../Stores/SessionStore.js';
  import Card from '../components/Card.svelte';
  import EditableTitle from '../components/EditableTitle.svelte';
  import FabButton from '../components/FabButton.svelte';

  let columns = [];
  let retroTitle = '';

  cardsStore.subscribe((value) => void (columns = value));
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
    margin-top: 1rem;
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
</style>

<div class="w-full sm:w-1/3">
  <EditableTitle title={retroTitle} {updateTitle}>
    <h2 class="text-lg text-gray-700 hover:text-teal-600 leading-snug">
      {retroTitle}
    </h2>
  </EditableTitle>
</div>
<div class="w-full h-full relative left-shadow right-shadow">
  <div
    class="mt-4 overflow-x-scroll scrollable-container h-full pb-2 pl-1 flex
    h-full">
    {#each columns as card, idx}
      <div class="w-full min-w-13 {getMargin(idx)}">
        <Card {card} />
      </div>
    {/each}
  </div>
</div>
<FabButton {onClick} />
