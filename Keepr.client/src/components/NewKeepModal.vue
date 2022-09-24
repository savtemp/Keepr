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
          <KeepForm />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import KeepForm from "./KeepForm.vue";
export default {
  setup() {
    return {};
  },
  components: { KeepForm },
};
</script>

<style lang="scss" scoped></style>
