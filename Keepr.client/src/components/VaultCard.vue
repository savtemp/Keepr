<template>
  <div class="rounded border elevation-2 selectable no-select">
    <router-link :to="{ name: 'VaultDetails', params: { vaultId: vault.id } }">
      <img class="img-fluid cover-img" :src="vault.img" alt="" />
      <p>{{ vault.name }}</p>
    </router-link>
  </div>
</template>

<script>
export default {
  props: { vault: { type: Object, required: true } },
  setup() {
    return {};
  },
};
</script>

<style lang="scss" scoped></style>
