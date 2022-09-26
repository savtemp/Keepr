<!-- TODO set up VaultKeep modal to view keeps in vaults -->

<template>
  <div class="container-fluid">
    <h1>Hello from the VaultDetails Page</h1>
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
      vaultKeep: computed(() => AppState.activeVaultKeep),

      async deleteVault() {
        try {
          await vaultsService.deleteVault(route.params.vaultId);
          Pop.toast("Vault Deleted", "success");
        } catch (error) {
          Pop.error(error.message);
          logger.log(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped></style>
