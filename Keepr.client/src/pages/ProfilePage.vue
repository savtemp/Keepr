<template>
  <!-- v-if="profile" -->
  <div class="container-fluid">
    <div class="row m-3 pt-4">
      <img
        class="col-md-3 img-fluid profile-img rounded"
        :src="profile.picture"
        alt=""
      />
      <div class="col-md-4">
        <p class="fs-1 m-0">{{ profile.name }}</p>
        <p class="fs-4 m-0">Vaults: {{ vaults.length }}</p>
        <p class="fs-4 m-0">Keeps: {{ keeps.length }}</p>
      </div>
    </div>
    <div class="row m-3">
      <!-- TODO users pushed back to home page when they try to open a private vault -->
      <div class="col-md-12 fs-2 pt-5">
        <div class="d-flex">
          <p>Vaults</p>
          <div v-if="profile.id == account.id">
            <i
              @click="newVault"
              class="selectable no-select mdi mdi-plus text-primary"
              title="Create Vault"
            ></i>
          </div>
        </div>
      </div>
      <!-- TODO might make this a fake carousel -->
      <div class="col-md-2" v-for="v in vaults" :key="v.id">
        <VaultCard class="selectable no-select" :vault="v" />
      </div>
    </div>
    <div class="row m-3">
      <div class="col-md-12 fs-2">
        <div class="d-flex pt-4">
          <p>Keeps</p>
          <div v-if="profile.id == account.id">
            <i
              @click="newKeep"
              class="selectable no-select mdi mdi-plus text-primary"
              title="Create Keep"
            ></i>
          </div>
        </div>
      </div>
    </div>
    <div class="masonry mx-3">
      <div class="selectable no-select" v-for="k in keeps" :key="k.id">
        <ProfileKeepCard :keep="k" />
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
      activeVault: computed(() => AppState.activeVault),

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
.profile-img {
  height: 140px;
  width: 170px;
  object-fit: cover;
}
.cover-img {
  height: 150px;
  width: 150px;
  object-fit: cover;
}
.masonry {
  columns: 4;
  column-gap: 16px;
  @media (max-width: 1200px) {
    columns: 3;
  }
  @media (max-width: 992px) {
    columns: 2;
  }
  //@media (max-width: 768px) {columns: 1;}
}
</style>
