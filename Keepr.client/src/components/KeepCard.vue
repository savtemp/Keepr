<template>
  <div
    class="rounded border elevation-2 selectable no-select"
    @click="setActive"
  >
    <div class="p-1">
      <img class="img-fluid" :src="keep.img" alt="" />
      <p>{{ keep.name }}</p>
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

<style lang="scss" scoped></style>
