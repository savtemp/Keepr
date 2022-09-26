<!-- TODO create searchbar for top of homepage - pull from TheNetwork example -->

<template>
  <div class="container-fluid">
    <div class="masonry">
      <div v-for="k in keeps" :key="k.id">
        <KeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import KeepCard from "../components/KeepCard.vue";
import Pop from "../utils/Pop.js";

export default {
  name: "Home",
  setup() {
    onMounted(() => {
      getKeeps();
    });

    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      } catch (error) {
        Pop.log(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { KeepCard },
};
</script>

<style scoped lang="scss">
.masonry {
  columns: 200px;
  column-gap: 1em;

  div {
    display: block;
    margin-bottom: 1em;
  }
}
</style>
