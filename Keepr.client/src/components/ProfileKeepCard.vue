<template>
  <div
    @click="setActive"
    class="rounded border elevation-2 selectable no-select"
  >
    <div class="p-1">
      <img class="img-fluid" :src="keep.img" alt="" />
      <p>{{ keep.name }}</p>
    </div>
  </div>
  <ProfileKeepModal />
</template>

<script>
import { Modal } from "bootstrap";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import ProfileKeepModal from "./ProfileKeepModal.vue";

export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    return {
      async setActive() {
        try {
          Modal.getOrCreateInstance(
            document.getElementById("profileKeepModal")
          ).toggle();
          await keepsService.getById(props.keep.id);
        } catch (error) {
          logger.log(error);
        }
      },
    };
  },
  components: { ProfileKeepModal },
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
