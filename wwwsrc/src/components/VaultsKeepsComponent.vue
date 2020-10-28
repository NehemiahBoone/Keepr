<template>
  <div class="card">
    <img
      class="card-img-top"
      :src="vaultKeepProp.img"
      @click="setActive"
      data-toggle="modal"
      :data-target="'#' + modalId"
      alt=""
    />
    <h2 class="text-white">
      {{ vaultKeepProp.name }}
      <i
        class="fas fa-minus-circle text-warning"
        v-if="this.$auth.userInfo.id == this.$route.params.profileId"
        @click="removeKeep"
        aria-hidden="true"
      ></i>
    </h2>

    <keep-modal :id="modalId">
      <template v-slot:header>
        <div v-if="activeKeep.id">
          <h2 class="text-white">{{ activeKeep.name }}</h2>
          <small
            >Posted By:
            <span class="text-info">{{ activeKeep.creator.name }}</span></small
          >
          <br />
          <br />
          <p class="text-light">{{ activeKeep.description }}</p>
        </div>
      </template>

      <template v-slot:body>
        <div v-if="activeKeep.id">
          <div class="text-center">
            <img class="img-fluid" :src="activeKeep.img" alt="" />
          </div>
          <small class="d-block text-center"
            >Views: {{ activeKeep.views }} <b>|</b> Keeps:
            {{ activeKeep.keeps }}</small
          >
          <br />
          <div class="text-center">
            <img class="creator-img" :src="activeKeep.creator.picture" alt="" />
          </div>
          <br />
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
      this.vaultKeepProp.views++;
      this.$store.dispatch("viewKeep", this.vaultKeepProp);
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
.creator-img {
  border-radius: 50%;
}
</style>