<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <h2>{{ activeVault.name }}</h2>
        <h5>{{ vaultsKeeps.length }}</h5>
      </div>
    </div>
    <div class="row">
      <vaults-keeps-component
        v-for="keep in vaultsKeeps"
        :key="keep.id"
        :vaultKeepProp="keep"
      />
    </div>
  </div>
</template>

<script>
import VaultsKeepsComponent from "../components/VaultsKeepsComponent.vue";
export default {
  mounted() {
    this.$store.dispatch("getActiveVault", this.$route.params.vaultId);
    this.$store.dispatch("getKeepsByVaultId", this.$route.params.vaultId);
  },
  name: "Vault",
  computed: {
    activeVault() {
      return this.$store.state.activeVault;
    },
    vaultsKeeps() {
      return this.$store.state.currentVaultsKeeps;
    },
  },
  components: {
    VaultsKeepsComponent,
  },
};
</script>

<style>
</style>