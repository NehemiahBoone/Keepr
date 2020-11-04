<template>
  <div class="container-fluid">
    <div class="row my-2">
      <div class="col-12 text-center">
        <img class="creator-img" :src="viewedProfile.picture" alt="" />
        <h2>{{ viewedProfile.name }}</h2>
        <h5>Vaults: {{ vaults.length }}</h5>
        <h5>Keeps: {{ keeps.length }}</h5>
      </div>
    </div>

    <div class="row my-1">
      <div class="col-12">
        <h3>
          Vaults
          <i
            v-if="this.$auth.userInfo.id == this.$route.params.id"
            class="fas fa-plus text-success"
            @click="vaultToggle = !vaultToggle"
          ></i>
        </h3>
        <form
          class="form-inline"
          v-if="vaultToggle"
          @submit.prevent="createVault"
        >
          <div class="form-group">
            <input
              type="text"
              name="vaultName"
              id="vaultName"
              v-model="newVault.name"
              class="form-control"
              placeholder="Title..."
              aria-describedby="helpId"
            />
            <input
              type="text"
              name="vaultDescription"
              id="vaultDescription"
              v-model="newVault.description"
              class="form-control"
              placeholder="Description..."
              aria-describedby="helpId"
            />
            <label for="vaultIsPrivate">Private</label>
            <input
              type="checkbox"
              name="vaultIsPrivate"
              id="vaultIsPrivate"
              v-model="newVault.IsPrivate"
              class="form-control"
              aria-describedby="helpId"
            />
          </div>
          <button class="btn btn-success" type="submit">Create Vault</button>
        </form>
      </div>
    </div>

    <div class="row mb-4 mx-1">
      <vault-component
        v-for="vault in vaults"
        :key="vault.id"
        :vaultProp="vault"
      />
    </div>

    <div class="row my-1">
      <div class="col-12">
        <h3>
          Keeps
          <i
            v-if="this.$auth.userInfo.id == this.$route.params.id"
            class="fas fa-plus text-success"
            @click="keepToggle = !keepToggle"
          ></i>
        </h3>
        <form
          class="form-inline"
          v-if="keepToggle"
          @submit.prevent="createKeep"
        >
          <div class="form-group">
            <input
              type="text"
              name="keepName"
              id="keepName"
              v-model="newKeep.name"
              class="form-control"
              placeholder="Title..."
              aria-describedby="helpId"
            />
            <input
              type="text"
              name="keepImg"
              id="keepImg"
              v-model="newKeep.img"
              class="form-control"
              placeholder="URL..."
              aria-describedby="helpId"
            />
            <input
              type="text"
              name="keepDescription"
              id="keepDescription"
              v-model="newKeep.description"
              class="form-control"
              placeholder="Description..."
              aria-describedby="helpId"
            />
          </div>
          <button class="btn btn-success" type="submit">Create Keep</button>
        </form>
      </div>
    </div>

    <div class="card-columns">
      <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep" />
    </div>
  </div>
</template>

<script>
import keepComponent from "../components/KeepComponent";
import vaultComponent from "../components/VaultComponent";
export default {
  mounted() {
    this.$store.dispatch("getSearchedProfile", this.$route.params.id);
    this.$store.dispatch("getProfileVaults", this.$route.params.id);
    this.$store.dispatch("getProfileKeeps", this.$route.params.id);
  },
  name: "profile",
  data() {
    return {
      keepToggle: false,
      vaultToggle: false,
      newVault: {},
      newKeep: {},
    };
  },
  computed: {
    keeps() {
      return this.$store.state.profileKeeps;
    },
    vaults() {
      return this.$store.state.profileVaults;
    },
    viewedProfile() {
      return this.$store.state.viewedProfile;
    },
  },
  methods: {
    createKeep() {
      this.$store.dispatch("createKeep", {
        newKeep: this.newKeep,
        profileId: this.$route.params.id,
      });
      this.newKeep = {};
    },
    createVault() {
      this.$store.dispatch("createVault", {
        newVault: this.newVault,
        profileId: this.$route.params.id,
      });
      this.vaultToggle = false;
      this.newVault = {};
    },
  },
  components: {
    keepComponent,
    vaultComponent,
  },
};
</script>

<style>
.creator-img {
  border-radius: 50%;
}
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