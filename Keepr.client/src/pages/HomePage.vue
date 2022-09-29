<!-- TODO create searchbar for top of homepage - pull from TheNetwork example -->

<template>
  <body class="container-fluid masonry mt-5 px-5">
    <div class="selectable no-select" v-for="k in keeps" :key="k.id">
      <KeepCard :keep="k" />
    </div>
  </body>
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
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps),
      // account: computed(() => AppState.account),
    };
  },
  components: { KeepCard },
};
</script>

<style scoped lang="scss">
.masonry {
  columns: 4;
  column-gap: 16px;
  @media (max-width: 1200px) {
    columns: 3;
  }
  @media (max-width: 992px) {
    columns: 2;
  }
  @media (max-width: 768px) {
    columns: 1;
  }
}
</style>
