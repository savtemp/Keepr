<template>
  <div class="grid" @click="setActive">
    <img class="hero-img" :src="keep.img" alt="" />

    <div class="grid__body">
      <div class="d-flex justify-content-between" @click.stop="profilePage">
        <p class="grid__title m-0">{{ keep.name }}</p>
        <img
          class="profile-img"
          :src="keep.creator.picture"
          alt=""
          :title="keep.creator.name"
        />
      </div>
    </div>
  </div>
  <KeepModal />
</template>

<script>
import { Modal } from "bootstrap";
import { router } from "../router.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
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

      async profilePage() {
        try {
          router.push({
            name: "Profile",
            params: { profileId: props.keep.creatorId },
          });
        } catch (error) {
          Pop.error(error);
          logger.log(error);
        }
      },
    };
  },
  components: { KeepModal },
};
</script>

<style lang="scss" scoped>
.hero-img {
  width: 100%;
  border-radius: 5px;
}
.profile-img {
  height: 40px;
  width: 40px;
  object-fit: cover;
  border-radius: 50%;
}

.grid {
  display: inline-block;
  margin-bottom: 16px;
  position: relative;
  &:before {
    border-radius: 5px;
    content: "";
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    background-color: rgba(0, 0, 0, 0.2);
  }
  &__title {
    font-size: 28px;
    font-weight: bold;
    text-shadow: 10px 10px 15px black;
    margin: 0px 0px 10px 0px;
  }
  &__author {
    font-size: 14px;
    font-weight: 300;
  }
  &__link {
    position: absolute;
    left: 0;
    right: 0;
    top: 0;
    bottom: 0;
  }
  &__body {
    position: absolute;
    left: 0;
    right: 0;
    top: 0;
    bottom: 0;
    padding: 10px 15px;
    color: #fff;
    display: flex;
    flex-direction: column;
    justify-content: flex-end;
  }
  &__tag {
    background-color: rgba(255, 255, 255, 0.8);
    color: #333;
    border-radius: 5px;
    padding: 5px 15px;
    margin-bottom: 5px;
  }
}
</style>
