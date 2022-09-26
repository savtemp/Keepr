<template>
  <div
    class="modal fade"
    id="profileKeepModal"
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
              <footer class="row justify-content-around">
                <div class="col-md-4">
                  <button class="btn btn-secondary" type="button">
                    Remove from Vault
                  </button>
                </div>
                <div class="col-md-1 text-center">
                  <i class="fs-3 mdi mdi-delete-outline"></i>
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

export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep),
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
