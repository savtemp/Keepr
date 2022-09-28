<template>
  <div class="rounded border elevation-2 selectable no-select">
    <!-- <router-link :to="{ name: 'VaultDetails', params: { vaultId: vault.id } }">
      <img class="img-fluid cover-img" :src="vault.img" alt="" />
      <p>{{ vault.name }}</p>
    </router-link> -->
    <div @click="vaultDetailsPage">
      <img class="img-fluid cover-img" :src="vault.img" alt="" />
      <p>{{ vault.name }}</p>
    </div>
  </div>
</template>

<script>
import { router } from "../router.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: { vault: { type: Object, required: true } },
  setup(props) {
    return {
      async vaultDetailsPage() {
        try {
          if (props.vault.creatorId) {
            await vaultsService.getVaultById(props.vault.id);
            router.push({
              name: "VaultDetails",
              params: { vaultId: props.vault.id },
            });
          }
        } catch (error) {
          if (props.vault.isPrivate == true) {
            router.push({ name: "Home" });
            Pop.error("You do not have access to this Vault.");
          }
          logger.log(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped></style>
