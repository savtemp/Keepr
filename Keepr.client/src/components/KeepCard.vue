<template>
  <div class="rounded border elevation-2 selectable no-select">
    <div class="p-1">
      <img class="img-fluid" :src="keep.img" alt="" @click="setActive" />
      <p>{{ keep.name }}</p>
      <router-link
        :to="{ name: 'Profile', params: { profileId: keep.creator.id } }"
      >
        <img class="img-fluid profile-img" :src="keep.creator.picture" alt="" />
      </router-link>
    </div>
  </div>
  <KeepModal />
</template>

<script>
import { Modal } from "bootstrap";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import KeepModal from "./KeepModal.vue";

export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    return {
      async setActive() {
        try {
          Modal.getOrCreateInstance(
            document.getElementById("keepModal")
          ).toggle();
          await keepsService.getById(props.keep.id);
        } catch (error) {
          logger.log(error);
        }
      },
    };
  },
  components: { KeepModal },
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
