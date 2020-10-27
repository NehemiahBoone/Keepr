<template>
  <div
    class="col-6 col-md-3 card"
    data-toggle="modal"
    :data-target="'#' + modalId"
    @click="setActive"
  >
    <img :src="vaultKeepProp.img" alt="" />
    <h2>{{ vaultKeepProp.name }}</h2>
    <i
      class="fa fa-trash"
      v-if="this.$auth.userInfo.id == this.$route.params.profileId"
      @click="removeKeep"
      aria-hidden="true"
    ></i>

    <keep-modal :id="modalId">
      <template v-slot:header>
        <div v-if="activeKeep.id">
          <h2 class="text-primary">{{ activeKeep.name }}</h2>
        </div>
      </template>

      <template v-slot:body>
        <div v-if="activeKeep.id">
          <img :src="activeKeep.img" alt="" />
          <p>{{ activeKeep.description }}</p>
        </div>
      </template>

      <template v-slot:footer>
        <div v-if="activeKeep.id">
          <small>Views: {{ activeKeep.views }}</small>
          <br />
          <small>Keeps: {{ activeKeep.keeps }}</small>
          <br />
          <br />
          <img :src="activeKeep.creator.picture" alt="" />
          <p>{{ activeKeep.creator.name }}</p>
        </div>
      </template>
    </keep-modal>
  </div>
</template>

<script>
import KeepModal from "./KeepModal.vue";
import VaultDropDownComponent from "./VaultDropDownComponent.vue";
export default {
  name: "VaultsKeepsComponent",
  props: ["vaultKeepProp"],
  computed: {
    modalId() {
      return "modal" + this.vaultKeepProp.id;
    },
    activeKeep() {
      return this.$store.state.activeKeep;
    },
  },
  methods: {
    viewProfile() {
      this.$router.push({
        name: "Profile",
        params: { id: this.vaultKeepProp.creator.id },
      });
    },
    viewKeep() {
      let viewedKeep = this.vaultKeepProp;
      viewedKeep.views++;
      this.$store.dispatch("viewKeep", viewedKeep);
    },
    removeKeep() {
      let vaultKeepToDelete = {
        vaultKeepId: this.vaultKeepProp.vaultKeepId,
        vaultId: this.$route.params.vaultId,
      };
      console.log(vaultKeepToDelete);
      let popup = confirm("Are you sure you want to remove this keep?");
      if (popup == true) {
        this.$store.dispatch("removeKeep", vaultKeepToDelete);
      }
    },
    setActive() {
      console.log(this.vaultKeepProp);
      this.$store.dispatch("setActiveKeep", this.vaultKeepProp);
    },
    sendKeepId() {
      this.$store.dispatch("sendKeepId", this.vaultKeepProp.id);
    },
  },
  components: {
    KeepModal,
    VaultDropDownComponent,
  },
};
</script>

<style>
</style>