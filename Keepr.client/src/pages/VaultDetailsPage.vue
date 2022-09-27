<!-- TODO set up VaultKeep modal to view keeps in vaults -->

<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-9">
        <p class="fs-1">{{ vault.name }}</p>
        <p>
          Keeps: <span>{{ vault.kept }}</span>
        </p>
      </div>
      <div class="col-md-3">
        <button class="btn btn-danger" @click="deleteVault">
          Delete Vault
        </button>
      </div>
    </div>
    <div class="row">
      <div class="col-md-3" v-for="k in keeps" :key="k.id">
        <VaultKeepCard :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import VaultKeepCard from "../components/VaultKeepCard.vue";
import { router } from "../router.js";

export default {
  setup() {
    const route = useRoute();
    onMounted(() => {
      getVaultById();
      getVaultKeeps();
    });
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.vaultId);
      } catch (error) {
        Pop.error(error.message);
        logger.log(error);
      }
    }
    async function getVaultKeeps() {
      try {
        await vaultsService.getVaultKeeps(route.params.vaultId);
      } catch (error) {
        Pop.error(error.message);
        logger.log(error);
      }
    }
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps),

      async deleteVault() {
        try {
          await vaultsService.deleteVault(route.params.vaultId);
          Pop.toast("Vault Deleted", "success");
          router.push({ name: "Home" });
        } catch (error) {
          Pop.error(error.message);
          logger.log(error);
        }
      },
    };
  },
  components: { VaultKeepCard },
};
</script>

<style lang="scss" scoped>
.cover-img {
  height: 150px;
  width: 150px;
  object-fit: cover;
}
</style>
