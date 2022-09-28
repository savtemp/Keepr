<template>
  <div
    class="modal fade"
    id="keepModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body">
          <div class="row">
            <div class="col-6">
              <img class="img-fluid" :src="keep?.img" alt="" />
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
                  <i class="mdi mdi-eye">{{ keep?.views }}</i>
                  <i class="mdi mdi-alpha-k-box-outline">{{ keep?.kept }}</i>
                  <i class="mdi mdi-share-variant-outline">{{
                    keep?.shares
                  }}</i>
                </div>
              </div>
              <div class="row">
                <div class="col-12">
                  <p>{{ keep?.name }}</p>
                </div>
                <div class="col-10">
                  <p>{{ keep?.description }}</p>
                </div>
              </div>
              <footer class="row justify-content-around" v-if="account.id">
                <div class="col-md-4">
                  <button @click="removeKeepFromVault()" class="btn btn-danger">
                    Remove From Vault
                  </button>
                  <div class="dropdown">
                    <button
                      class="btn btn-success dropdown-toggle"
                      type="button"
                      data-bs-toggle="dropdown"
                      aria-expanded="false"
                    >
                      Add To Vault
                    </button>
                    <ul class="dropdown-menu">
                      <li v-for="v in vaults" :key="v.id">
                        <a
                          class="dropdown-item"
                          href="#"
                          data-bs-dismiss="modal"
                          @click="addToVault(keep.id, v.id)"
                          :vault="v"
                          >{{ v.name }}</a
                        >
                      </li>
                    </ul>
                  </div>
                </div>
                <div
                  class="col-md-1 text-center"
                  v-if="keep.creatorId == account.id"
                >
                  <i
                    class="fs-3 mdi mdi-delete-outline"
                    @click="deleteKeep(keep.id)"
                    data-bs-dismiss="modal"
                  ></i>
                </div>
                <div class="col-md-6">
                  <div class="row" data-bs-dismiss="modal">
                    <router-link
                      class="col-12 d-flex"
                      v-if="keep?.id"
                      :to="{
                        name: 'Profile',
                        params: { profileId: keep?.creator?.id },
                      }"
                    >
                      <div>
                        <img
                          class="img-fluid profile-img"
                          :src="keep?.creator?.picture"
                          alt=""
                        />
                      </div>
                      <div>
                        <p>{{ keep?.creator?.name }}</p>
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
import { AppState } from "../AppState.js";
import { router } from "../router.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      // vaultKeep: computed(() => AppState.activeVaultKeep),

      async deleteKeep(id) {
        try {
          await keepsService.deleteKeep(id);
          Pop.toast("Keep Deleted", "success");
          router.push({ name: "Home" });
        } catch (error) {
          Pop.error("You cannot delete a Keep that is not yours.", "warning");
          logger.log(error);
        }
      },

      async addToVault(keepId, vaultId) {
        try {
          let newVaultKeep = { keepId, vaultId };
          await vaultKeepsService.createVaultKeep(newVaultKeep);
          logger.log(newVaultKeep);
          Pop.toast("Keep added to Vault!", "success");
        } catch (error) {
          Pop.error(error.message);
          logger.log(error);
        }
      },

      async removeKeepFromVault() {
        try {
          await vaultKeepsService.deleteVaultKeep(
            keep.vaultKeepViewModel.vaultKeepId
          );
          Pop.toast("Keep removed from Vault.");
          router.push({
            name: "Profile",
            params: { profileId: keep.creator.id },
          });
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
