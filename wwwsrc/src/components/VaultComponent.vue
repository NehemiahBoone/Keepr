<template>
  <div class="col-6 col-md-3 card">
    <h2 @click="viewVault">{{ vaultProp.name }}</h2>
    <i
      class="fa fa-trash text-danger"
      v-if="vaultProp.creatorId == this.$auth.userInfo.id"
      @click="deleteVault"
      aria-hidden="true"
    ></i>
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
        params: {
          profileId: this.vaultProp.creatorId,
          vaultId: this.vaultProp.id,
        },
      });
    },
    deleteVault() {
      let popup = confirm("Are you sure you want to delete this vault?");
      if (popup == true) {
        this.$store.dispatch("deleteVault", {
          vaultId: this.vaultProp.id,
          profileId: this.vaultProp.creatorId,
        });
      }
    },
  },
};
</script>

<style>
</style>