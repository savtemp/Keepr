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
                  <i class="fs-3 mdi mdi-window-close" title="Close Window"></i>
                </div>
              </header>
              <div class="row">
                <div class="col-12 text-center d-flex justify-content-center">
                  <div class="d-flex px-2">
                    <i
                      class="px-1 text-primary mdi mdi-eye"
                      title="Keep Views"
                    ></i>
                    <p>{{ vaultKeepViewModel?.views }}</p>
                  </div>
                  <div class="d-flex px-2">
                    <i
                      class="px-1 text-primary mdi mdi-alpha-k-box-outline"
                      title="Amount Kept"
                    ></i>
                    <p>{{ vaultKeepViewModel?.kept }}</p>
                  </div>
                  <div class="d-flex px-2">
                    <i
                      class="px-1 text-primary mdi mdi-share-variant-outline"
                      title="Keep Shares"
                    ></i>
                    <p>{{ vaultKeepViewModel?.shares }}</p>
                  </div>
                </div>
              </div>
              <div class="row keep-body">
                <div class="col-12 d-flex flex-column justify-content-between">
                  <div class="row">
                    <div class="col-md-12 pt-4">
                      <p class="fs-1 text-center">
                        {{ vaultKeepViewModel?.name }}
                      </p>
                      <p class="pt-3">{{ vaultKeepViewModel?.description }}</p>
                    </div>
                  </div>
                  <footer class="row justify-content-around">
                    <div
                      class="col-md-4"
                      v-if="vaultKeepViewModel?.creator?.id == account.id"
                    >
                      <button
                        @click="
                          removeKeepFromVault(vaultKeepViewModel.vaultKeepId)
                        "
                        class="btn btn-danger"
                        data-bs-dismiss="modal"
                        title="Remove Keep"
                      >
                        Remove From Vault
                      </button>
                    </div>
                    <div class="col-md-6">
                      <div class="row" data-bs-dismiss="modal">
                        <router-link
                          class="col-12 d-flex justify-content-center"
                          v-if="vaultKeepViewModel?.id"
                          :to="{
                            name: 'Profile',
                            params: {
                              profileId: vaultKeepViewModel?.creator?.id,
                            },
                          }"
                        >
                          <div class="px-2">
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
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    return {
      vaultKeepViewModel: computed(() => AppState.activeVaultKeep),
      account: computed(() => AppState.account),
      async removeKeepFromVault(id) {
        try {
          const yes = await Pop.confirm("Remove this Keep?");
          if (!yes) {
            return;
          }
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
.keep-body {
  min-height: 78%;
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
