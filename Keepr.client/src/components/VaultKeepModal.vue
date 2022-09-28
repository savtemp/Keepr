<template>
  <div
    class="modal fade"
    id="vaultKeepModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body">
          <div class="row">
            <div class="col-6">
              <img class="img-fluid" :src="vaultKeepViewModel?.img" alt="" />
            </div>
            <div class="col-6">
              <header class="row">
                <div
                  class="col-12 text-end"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                >
                  <i class="fs-3 mdi mdi-window-close"></i>
                </div>
              </header>
              <div class="row">
                <div class="col-12 text-center">
                  <i class="mdi mdi-eye">{{ vaultKeepViewModel?.views }}</i>
                  <i class="mdi mdi-alpha-k-box-outline">{{
                    vaultKeepViewModel?.kept
                  }}</i>
                  <i class="mdi mdi-share-variant-outline">{{
                    vaultKeepViewModel?.shares
                  }}</i>
                </div>
              </div>
              <div class="row">
                <div class="col-12">
                  <p>{{ vaultKeepViewModel?.name }}</p>
                </div>
                <div class="col-10">
                  <p>{{ vaultKeepViewModel?.description }}</p>
                </div>
              </div>
              <footer class="row justify-content-around">
                <div class="col-md-4">
                  <button
                    @click="removeKeepFromVault(vaultKeepViewModel.vaultKeepId)"
                    class="btn btn-danger"
                    data-bs-dismiss="modal"
                  >
                    Remove From Vault
                  </button>
                </div>
                <div class="col-md-6">
                  <div class="row" data-bs-dismiss="modal">
                    <router-link
                      class="col-12 d-flex"
                      v-if="vaultKeepViewModel?.id"
                      :to="{
                        name: 'Profile',
                        params: { profileId: vaultKeepViewModel?.creator?.id },
                      }"
                    >
                      <div>
                        <img
                          class="img-fluid profile-img"
                          :src="vaultKeepViewModel?.creator?.picture"
                          alt=""
                        />
                      </div>
                      <div>
                        <p>{{ vaultKeepViewModel?.creator?.name }}</p>
                      </div>
                    </router-link>
                  </div>
                </div>
              </footer>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import { router } from "../router.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    return {
      vaultKeepViewModel: computed(() => AppState.activeVaultKeep),

      async removeKeepFromVault(id) {
        try {
          console.log(AppState.vaultKeeps);
          await vaultKeepsService.deleteVaultKeep(id);
          Pop.toast("Keep removed from Vault.");
        } catch (error) {
          Pop.error(error.message);
          logger.log(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.modal-img {
  height: 650px;
  width: 600px;
  object-fit: cover;
}

.profile-img {
  height: 50px;
  width: 50px;
  object-fit: cover;
  border-radius: 50%;
  height: 2em;
  width: 2em;
}
</style>
