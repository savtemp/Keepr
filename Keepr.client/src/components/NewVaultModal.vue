<template>
  <div
    class="modal fade"
    id="vaultFormModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">New Vault</h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <!-- v-if="isCreator" -->
          <VaultForm :vaultData="vault" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import VaultForm from "./VaultForm.vue";
export default {
  setup() {
    return {
      vault: computed(() => AppState.activeVault),
      isCreator: computed(
        () => AppState.activeKeep?.creatorId == AppState.activeProfile?.id
      ),
    };
  },
  components: { VaultForm },
};
</script>

<style lang="scss" scoped></style>
