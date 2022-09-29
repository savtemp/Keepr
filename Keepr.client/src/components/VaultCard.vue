<template>
  <div class="grid" @click="vaultDetailsPage">
    <img class="img-fluid hero-img" :src="vault.img" alt="" />
    <div class="grid__body">
      <p class="grid__title">{{ vault.name }}</p>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { router } from "../router.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: { vault: { type: Object, required: true } },
  setup(props) {
    let account = AppState.account;
    return {
      account,

      async vaultDetailsPage() {
        try {
          if (
            props.vault.creatorId == account.id ||
            props.vault.isPrivate == false
          ) {
            await vaultsService.getVaultById(props.vault.id);
            router.push({
              name: "VaultDetails",
              params: { vaultId: props.vault.id },
            });
          } else if (
            props.vault.isPrivate == true &&
            props.vault.creatorId != account.id
          ) {
            router.push({ name: "Home" });
            Pop.error("You do not have access to this Vault.");
          }
        } catch (error) {
          router.push({ name: "Home" });
          logger.log(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.hero-img {
  width: 180px;
  height: 180px;
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
    text-shadow: 10px 10px 15px black;
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
