<template>
  <form @submit.prevent="handleSubmit">
    <div>
      <label class="form-label" for="keep-name">Title</label>
      <input
        class="form-control"
        required
        type="text"
        name="keep-name"
        id="keep-name"
        placeholder="Title..."
        v-model="editable.name"
      />
    </div>
    <div>
      <label class="form-label" for="keep-img">Image Url</label>
      <input
        class="form-control"
        required
        type="text"
        name="keep-img"
        id="keep-img"
        placeholder="URL..."
        v-model="editable.img"
      />
    </div>
    <div>
      <label class="form-label" for="keep-description">Description</label>
      <textarea
        class="form-control"
        name=""
        id="keep-description"
        cols="30"
        rows="8"
        placeholder="Keep Description..."
        v-model="editable.description"
      ></textarea>
    </div>
    <div>
      <button
        class="btn btn-success"
        title="submit form"
        data-bs-dismiss="modal"
      >
        Submit
      </button>
      <!-- <button class="btn btn-warning" type="button" title="edit form">
        Edit
      </button> -->
    </div>
  </form>
</template>

<script>
import { ref } from "vue";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const editable = ref({});

    return {
      editable,

      async handleSubmit() {
        try {
          logger.log("form data", editable.value);
          if (!editable.value.id) {
            await keepsService.create(editable.value);
            editable.value = {};
            Pop.toast("Keep Created!", "warning");
            // }
            // else {
            //   await keepsService.update(editable.value);
            //   Pop.toast("Keep Updated!", "info");
          }
        } catch (error) {
          logger.log(error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped></style>
