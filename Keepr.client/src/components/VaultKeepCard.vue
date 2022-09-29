<template>
  <div class="grid" @click="setActive">
    <img class="hero-img" :src="vaultKeepViewModel.img" alt="" />
    <div class="grid__body">
      <p class="grid__title m-0">{{ vaultKeepViewModel.name }}</p>
    </div>
  </div>
  <VaultKeepModal />
</template>

<script>
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
.hero-img {
  width: 100%;
  border-radius: 5px;
}

.grid {
  display: inline-block;
  margin-bottom: 16px;
  position: relative;
  &:before {
    border-radius: 5px;
    content: "";
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    background-color: rgba(0, 0, 0, 0.2);
  }
  &__title {
    font-size: 28px;
    font-weight: bold;
    margin: 0px 0px 10px 0px;
  }
  &__author {
    font-size: 14px;
    font-weight: 300;
  }
  &__link {
    position: absolute;
    left: 0;
    right: 0;
    top: 0;
    bottom: 0;
  }
  &__body {
    position: absolute;
    left: 0;
    right: 0;
    top: 0;
    bottom: 0;
    padding: 10px 15px;
    color: #fff;
    display: flex;
    flex-direction: column;
    justify-content: flex-end;
  }
  &__tag {
    background-color: rgba(255, 255, 255, 0.8);
    color: #333;
    border-radius: 5px;
    padding: 5px 15px;
    margin-bottom: 5px;
  }
}
</style>
