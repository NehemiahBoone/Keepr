<template>
  <div class="container-fluid">
    <div class="row mb-2" v-if="activeVault.name">
      <div class="col-12">
        <h2>{{ activeVault.name }}</h2>
        <p>Current Keeps: {{ vaultsKeeps.length }}</p>
      </div>
    </div>
    <div class="card-columns" v-if="activeVault.name">
      <vaults-keeps-component
        v-for="keep in vaultsKeeps"
        :key="keep.id"
        :vaultKeepProp="keep"
      />
    </div>

    <div class="row" v-if="!activeVault.name">
      <div class="col-12 text-center">
        <h2>THIS VAULT IS PRIVATE</h2>
        <button type="button" @click="goBackHome" class="btn btn-info">
          Go To Home
        </button>
      </div>
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
  methods: {
    goBackHome() {
      this.$router.push({ name: "Home" });
    },
  },
};
</script>

<style>
@media (min-width: 576px) {
  .card-columns {
    column-count: 2;
  }
}

@media (min-width: 768px) {
  .card-columns {
    column-count: 3;
  }
}

@media (min-width: 992px) {
  .card-columns {
    column-count: 4;
  }
}
</style>