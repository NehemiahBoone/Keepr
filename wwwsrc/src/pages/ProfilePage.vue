<template>
  <div class="container-fluid">
    <div class="row my-5">
      <div class="col-12">
        <div class="row">
          <div class="col-sm-12 col-md-2">
            <img :src="viewedProfile.picture" alt="" />
          </div>
          <div class="col-sm-12 col-md-10">
            <h2>{{ viewedProfile.name }}</h2>
            <h5>Vaults: {{ vaults.length }}</h5>
            <h5>Keeps: {{ keeps.length }}</h5>
          </div>
        </div>
      </div>
    </div>
    <div class="row my-1">
      <div class="col-12">
        <h3>
          Keeps
          <i
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
    <div class="row mb-4">
      <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep" />
    </div>
    <div class="row my-1">
      <div class="col-12">
        <h3>Vaults <i class="fas fa-plus text-success"></i></h3>
      </div>
    </div>
    <div class="row mb-4">
      <vault-component
        v-for="vault in vaults"
        :key="vault.id"
        :vaultProp="vault"
      />
    </div>
  </div>
</template>

<script>
import keepComponent from "../components/KeepComponent";
import vaultComponent from "../components/VaultComponent";
export default {
  mounted() {
    this.$store.dispatch("getSearchedProfile", this.$route.params.id);
    this.$store.dispatch("getProfileKeeps", this.$route.params.id);
    this.$store.dispatch("getProfileVaults", this.$route.params.id);
  },
  name: "profile",
  data() {
    return {
      keepToggle: false,
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
  },
  components: {
    keepComponent,
    vaultComponent,
  },
};
</script>

<style>
</style>