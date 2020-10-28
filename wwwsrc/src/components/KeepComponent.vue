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
    <h2 class="text-white">
      {{ keepProp.name }}
      <img
        @click="viewProfile"
        :src="keepProp.creator.picture"
        class="creator-img"
        height="50"
        width="50"
        alt=""
      />
      <i
        class="fa fa-trash text-danger"
        v-if="keepProp.creatorId == this.$auth.userInfo.id"
        aria-hidden="true"
        @click="deleteKeep"
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
          <div class="dropdown show text-center">
            <button
              class="btn btn-success dropdown-toggle"
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
.card {
  box-shadow: 0px 0px 0px grey;
  -webkit-transition: box-shadow 0.6s ease-out;
  box-shadow: 0.8px 0.9px 3px grey;
}
.card:hover {
  box-shadow: 1px 8px 20px grey;
  -webkit-transition: box-shadow 0.6s ease-in;
}
.creator-img {
  border-radius: 50%;
}
</style>