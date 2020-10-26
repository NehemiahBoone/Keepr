<template>
  <div class="col-6 col-md-3 card" @click="viewVault">
    <h2>{{ vaultProp.name }}</h2>
  </div>
</template>

<script>
export default {
  name: "vault-component",
  props: ["vaultProp"],
  methods: {
    viewVault() {
      this.$store.dispatch("setActiveVault", this.vaultProp);
      this.$router.push({
        name: "Vault",
        params: { vaultId: this.vaultProp.id },
      });
    },
  },
};
</script>

<style>
</style>