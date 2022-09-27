<template>
  <form @submit.prevent="handleSubmit">
    <div>
      <label class="form-label" for="vault-name">Title</label>
      <input
        class="form-control"
        required
        type="text"
        name="vault-name"
        id="vault-name"
        placeholder="Title..."
        v-model="editable.name"
      />
    </div>
    <div>
      <label class="form-label" for="vault-img">Image Url</label>
      <input
        class="form-control"
        required
        type="text"
        name="vault-img"
        id="vault-img"
        placeholder="URL..."
        v-model="editable.img"
      />
    </div>
    <div>
      <label class="form-label" for="vault-description">Description</label>
      <textarea
        class="form-control"
        name=""
        id="vault-description"
        cols="30"
        rows="8"
        placeholder="Vault Description..."
        v-model="editable.description"
      ></textarea>
    </div>
    <div class="checkbox">
      <label class="form-control" for="isPrivate">
        <input
          type="checkbox"
          name="isPrivate"
          id="isPrivate"
          v-model="editable.isPrivate"
        />Private?</label
      >
      <p>Private Vaults can only be seen by you</p>
    </div>
    <div>
      <button
        class="btn btn-success"
        type="submit"
        title="submit form"
        data-bs-dismiss="modal"
      >
        Submit
      </button>
    </div>
  </form>
</template>

<script>
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { ref } from "vue";

export default {
  setup() {
    const editable = ref({ isPrivate: false });
    return {
      editable,

      async handleSubmit() {
        try {
          logger.log("form data", editable.value);
          if (!editable.value.id) {
            await vaultsService.create(editable.value);
            editable.value = {};
            Pop.toast("Vault Created!", "warning");
            // } else {
            //   await vaultsService.edit(editable.value);
            //   Pop.toast("Vault Edited", "info");
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
