<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-12 d-flex">
        <img class="img-fluid" :src="profile.picture" alt="" />
        <div>
          <p>{{ profile.name }}</p>
          <p>Vaults:</p>
          <p>Keeps:</p>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-12 fs-2">
        <div class="d-flex">
          <p>Vaults</p>
          <i class="mdi mdi-plus"></i>
        </div>
      </div>
      <!-- TODO might make this a fake carousel -->
      <div class="col-md-3">
        <img
          class="img-fluid cover-img"
          src="https://thiscatdoesnotexist.com/"
          alt=""
        />
      </div>
    </div>
    <div class="row">
      <div class="col-md-12 fs-2">
        <!-- TODO v-if="profile.id == account.id" -->
        <div class="d-flex">
          <p>Keeps</p>
          <i @click="newKeep" class="mdi mdi-plus"></i>
        </div>
      </div>
      <!-- TODO this might need to be a container to do masonry -->
      <div class="col-md-3">
        <img
          class="img-fluid cover-img"
          src="https://thiscatdoesnotexist.com/"
          alt=""
        />
      </div>
    </div>
  </div>
  <NewKeepModal />
</template>

<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import NewKeepModal from "../components/NewKeepModal.vue";

export default {
  name: "ProfilePage",
  setup() {
    return {
      profile: computed(() => AppState.account),

      async newKeep() {
        try {
          Modal.getOrCreateInstance(
            document.getElementById("keepFormModal")
          ).toggle();
        } catch (error) {
          logger.log(error);
        }
      },
    };
  },
  components: { NewKeepModal },
};
</script>

<style scoped lang="scss">
.cover-img {
  height: 150px;
  width: 150px;
  object-fit: cover;
}
</style>
