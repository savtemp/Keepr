<template>
  <div
    class="modal fade"
    id="keepFormModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">New Keep</h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <!-- v-if="isCreator" -->
          <KeepForm :keepData="keep" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import KeepForm from "./KeepForm.vue";
export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep),
      isCreator: computed(
        () => AppState.activeKeep?.creatorId == AppState.activeProfile?.id
      ),
    };
  },
  components: { KeepForm },
};
</script>

<style lang="scss" scoped></style>
