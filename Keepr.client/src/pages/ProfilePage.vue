<template>
  <!-- v-if="profile" -->
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
      <!-- TODO users pushed back to home page when they try to open a private vault -->
      <div class="col-md-12 fs-2">
        <div class="d-flex">
          <p>Vaults</p>
          <i @click="newVault" class="mdi mdi-plus"></i>
        </div>
      </div>
      <!-- TODO might make this a fake carousel -->
      <div class="col-md-2" v-for="v in vaults" :key="v.id">
        <VaultCard :vault="v" />
      </div>
    </div>
    <div class="row">
      <div class="col-md-12 fs-2">
        <!-- TODO v-if="profile.id == account.id" OR v-if="isCreator"-->
        <div class="d-flex">
          <p>Keeps</p>
          <i @click="newKeep" class="mdi mdi-plus"></i>
        </div>
      </div>
      <!-- TODO this might need to be a container to do masonry -->
      <div class="col-md-3" v-for="k in keeps" :key="k.id">
        <ProfileKeepCard :keep="k" />
        <!-- <img class="img-fluid cover-img" :src="k.img" alt="" /> -->
      </div>
    </div>
  </div>
  <NewKeepModal />
  <NewVaultModal />
</template>

<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import NewKeepModal from "../components/NewKeepModal.vue";
import { profilesService } from "../services/ProfilesService.js";
import { onMounted } from "vue";
import Pop from "../utils/Pop.js";
import { useRoute } from "vue-router";
import NewVaultModal from "../components/NewVaultModal.vue";
import VaultCard from "../components/VaultCard.vue";
import ProfileKeepCard from "../components/ProfileKeepCard.vue";

export default {
  name: "ProfilePage",
  setup() {
    const route = useRoute();

    onMounted(() => {
      getProfileById();
      getProfileKeeps();
      getProfileVaults();
    });

    async function getProfileById() {
      try {
        await profilesService.getProfileById(route.params.profileId);
      } catch (error) {
        Pop.error(error.message);
        logger.log(error);
      }
    }

    async function getProfileKeeps() {
      try {
        await profilesService.getProfileKeeps(route.params.profileId);
      } catch (error) {
        Pop.error(error.message);
        logger.log(error);
      }
    }

    async function getProfileVaults() {
      try {
        await profilesService.getProfileVaults(route.params.profileId);
      } catch (error) {
        Pop.error(error.message);
        logger.log;
      }
    }

    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),

      async newVault() {
        try {
          Modal.getOrCreateInstance(
            document.getElementById("vaultFormModal")
          ).toggle();
        } catch (error) {
          logger.log(error);
        }
      },

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
  components: { NewKeepModal, NewVaultModal, VaultCard, ProfileKeepCard },
};
</script>

<style scoped lang="scss">
.cover-img {
  height: 150px;
  width: 150px;
  object-fit: cover;
}
</style>
