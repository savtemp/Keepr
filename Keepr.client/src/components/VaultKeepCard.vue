<template>
  <div class="rounded border elevation-2 selectable no-select">
    <div class="p-1">
      <img
        class="img-fluid"
        :src="vaultKeepViewModel.img"
        alt=""
        @click="setActive"
      />
      <p>{{ vaultKeepViewModel.name }}</p>
    </div>
  </div>
  <VaultKeepModal />
</template>

<script>
import { findDir } from "@vue/compiler-core";
import { Modal } from "bootstrap";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { logger } from "../utils/Logger.js";
import VaultKeepModal from "./VaultKeepModal.vue";

export default {
  props: { vaultKeepViewModel: { type: Object, required: true } },
  setup(props) {
    return {
      async setActive() {
        try {
          // console.log("what is a vaultKeepViewModel", props.vaultKeepViewModel);

          // let vaultKeep = props.vaultKeepViewModel;
          // let activeKeep = AppState.activeKeep;
          let activeVaultKeep = props.vaultKeepViewModel;
          console.log("what is an activeVaultKeep", activeVaultKeep);
          await vaultKeepsService.setActive(activeVaultKeep);
          activeVaultKeep.views++;
          Modal.getOrCreateInstance(
            document.getElementById("vaultKeepModal")
          ).toggle();
        } catch (error) {
          logger.log(error);
        }
      },
    };
  },
  components: { VaultKeepModal },
};
</script>

<style lang="scss" scoped>
.profile-img {
  height: 50px;
  width: 50px;
  object-fit: cover;
  border-radius: 50%;
  height: 2em;
  width: 2em;
}
</style>
