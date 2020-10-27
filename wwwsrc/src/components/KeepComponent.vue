<template>
  <div class="card">
    <img
      class="card-img-top"
      @click="setActive"
      :src="keepProp.img"
      alt=""
      data-toggle="modal"
      :data-target="'#' + modalId"
    />
    <h2>{{ keepProp.name }}</h2>
    <img @click="viewProfile" :src="keepProp.creator.picture" alt="" />
    <i
      class="fa fa-trash"
      v-if="keepProp.creatorId == this.$auth.userInfo.id"
      aria-hidden="true"
      @click="deleteKeep"
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
          <div class="dropdown show">
            <button
              class="btn btn-primary dropdown-toggle"
              role="button"
              data-toggle="dropdown"
              @click="sendKeepId"
            >
              Add To Vault
            </button>

            <div class="dropdown-menu">
              <vault-drop-down-component
                v-for="vault in vaults"
                :key="vault.id"
                :vaultProp="vault"
              />
            </div>
          </div>
        </div>
      </template>
    </keep-modal>
  </div>
</template>

<script>
import KeepModal from "./KeepModal.vue";
import VaultDropDownComponent from "./VaultDropDownComponent.vue";
export default {
  name: "keep-component",
  props: ["keepProp", "index"],
  computed: {
    modalId() {
      return "modal" + this.keepProp.id;
    },
    activeKeep() {
      return this.$store.state.activeKeep;
    },
    vaults() {
      return this.$store.state.userVaults;
    },
  },
  methods: {
    viewProfile() {
      this.$router.push({
        name: "Profile",
        params: { id: this.keepProp.creator.id },
      });
    },
    deleteKeep() {
      let popup = confirm("Are you sure you want to delete this keep?");
      if (popup == true) {
        if (this.$route.name == "Profile") {
          this.$store.dispatch("deleteKeepOnProfile", {
            keep: this.keepProp,
            profileId: this.$route.params.id,
          });
        } else {
          this.$store.dispatch("deleteKeepOnHome", this.keepProp.id);
        }
      }
    },
    setActive() {
      console.log(this.keepProp);
      this.keepProp.views++;
      this.$store.dispatch("viewKeep", this.keepProp);
    },
    sendKeepId() {
      this.$store.dispatch("sendKeepId", this.keepProp.id);
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