<template>
  <nav class="d-flex navbar-expand-lg navbar-dark bg-primary px-3 py-0">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <i class="text-center text-dark mdi mdi-alpha-k-box-outline"></i>
    </router-link>

    <!-- SECTION search form -->
    <!-- FIXME not popping error, not working -->
    <!-- <div class="search-form">
      <form @submit.prevent="searchKeeps">
        <div class="input-group">
          <input class="form-control" type="text" required v-model="query" />
          <button class="btn btn-outline-dark" type="submit">
            <i class="mdi mdi-magnify"></i>
          </button>
        </div>
      </form>
    </div> -->
    <button
      class="navbar-toggler"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="px-3 collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <!-- <router-link
            :to="{ name: 'Profile', params: { profileId: id } }"
            class="btn text-success lighten-30 selectable text-uppercase"
          >
            Profile
          </router-link> -->
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { ref } from "vue";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const query = ref("");
    return {
      query,

      async searchKeeps() {
        try {
          await keepsService.searchKeeps(query.value);
        } catch (error) {
          logger.error("[Searching Keeps]", error);
          Pop.error(error);
        }
      },
    };
  },
};
</script>

<style scoped>
i {
  font-size: 70px;
}

a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
