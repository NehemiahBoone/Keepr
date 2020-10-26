<template>
  <div>
    <p class="dropdown-item" @click="addToVault">{{ vaultProp.name }}</p>
  </div>
</template>

<script>
export default {
  name: "VaultDropDownComponent",
  props: ["vaultProp", "keepProp"],
  methods: {
    addToVault() {
      this.$store.dispatch("addToVault", {
        vaultId: this.vaultProp.id,
        keepId: this.vaultKeepKeepId,
      });
    },
  },
  computed: {
    vaultKeepKeepId() {
      return this.$store.state.vaultKeepKeepId;
    },
  },
};
</script>

<style>
</style>