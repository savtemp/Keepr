<!-- TODO set up VaultKeep modal to view keeps in vaults -->

<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-9">
        <p class="fs-1">{{ vault.name }}</p>
        <p>
          Keeps: <span>{{ vaultKeepViewModel.length }}</span>
        </p>
      </div>
      <div class="col-md-3">
        <!-- TODO potentially move this? -->
        <button
          v-if="vault.creatorId == profile.id"
          class="btn btn-danger"
          @click="deleteVault"
        >
          Delete Vault
        </button>
      </div>
    </div>
    <div class="row">
      <div
        class="col-md-3"
        v-for="vk in vaultKeepViewModel"
        :key="vk.vaultKeepId"
      >
        <VaultKeepCard :vaultKeepViewModel="vk" />
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
      getVaultKeeps();
    });

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
      keeps: computed(() => AppState.activeKeep),
      vaultKeepViewModel: computed(() => AppState.vaultKeeps),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),

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
