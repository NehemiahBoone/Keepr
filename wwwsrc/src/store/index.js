import Vue from "vue";
import Vuex from "vuex";
import router from "../router/index.js";
import { api } from "../services/AxiosService.js";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    profile: {},
    viewedProfile: {},
    profileKeeps: [],
    profileVaults: [],
    keeps: [],
    userVaults: [],
    activeKeep: {},
    vaultKeepKeepId: null
  },

  mutations: {
    setProfile(state, profile) {
      state.profile = profile;
    },

    setViewedProfile(state, profile) {
      state.viewedProfile = profile;
    },

    setKeeps(state, keeps) {
      state.keeps = keeps;
    },

    setProfileKeeps(state, keeps) {
      state.profileKeeps = keeps
    },

    setProfileVaults(state, vaults) {
      state.profileVaults = vaults
    },

    setActiveKeep(state, keep) {
      state.activeKeep = keep
    },

    setUserVaults(state, vaults) {
      state.userVaults = vaults;
    },

    sendKeepId(state, keepId) {
      state.vaultKeepKeepId = keepId
    }

  },

  actions: {
    async getProfile({ commit }) {
      try {
        let res = await api.get("profiles");
        commit("setProfile", res.data);
      } catch (error) {
        console.error(error);
      }
    },

    async getKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps")
        console.log("GETTING KEEPS")
        commit("setKeeps", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    async getSearchedProfile({ commit, dispatch }, profileId) {
      try {
        let res = await api.get("profiles/" + profileId)
        commit("setViewedProfile", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    async getProfileKeeps({ commit, dispatch }, profileId) {
      try {
        let res = await api.get("profiles/" + profileId + "/keeps")
        commit("setProfileKeeps", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    async getProfileVaults({ commit, dispatch }, profileId) {
      try {
        let res = await api.get("profiles/" + profileId + "/vaults")
        commit("setProfileVaults", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    async viewKeep({ commit, dispatch }, viewedKeep) {
      try {
        let res = await api.put("keeps/" + viewedKeep.id, viewedKeep)
      } catch (error) {
        console.error(error);
      }
    },

    async createKeep({ commit, dispatch }, keep) {
      try {
        let res = await api.post("keeps", keep.newKeep)
        dispatch("getProfileKeeps", keep.profileId)
      } catch (error) {
        console.error(error);
      }
    },

    async deleteKeepOnProfile({ commit, dispatch }, keep) {
      try {
        await api.delete("keeps/" + keep.keep.id)
        dispatch("getProfileKeeps", keep.profileId)
      } catch (error) {
        console.error(error);
      }
    },

    async deleteKeepOnHome({ commit, dispatch }, keepId) {
      try {
        await api.delete("keeps/" + keepId)
        dispatch("getKeeps")
      } catch (error) {
        console.error(error);
      }
    },

    setActiveKeep({ commit, dispatch }, keep) {
      commit("setActiveKeep", keep)
    },

    async getUserVaults({ commit, dispatch }, profileId) {
      try {
        let res = await api.get("profiles/" + profileId + "/vaults")
        commit("setUserVaults", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    sendKeepId({ commit, dispatch }, keepId) {
      console.log("GOT SENT KEEPID")
      commit("sendKeepId", keepId)
    },

    async addToVault({ commit, dispatch }, vaultKeep) {
      try {
        let res = await api.post("vaultkeeps", vaultKeep)
        dispatch("getKeepById", vaultKeep.keepId)
        dispatch("getKeepsByVaultId", vaultKeep.vaultId)
      } catch (error) {
        console.error(error);
      }
    },

    async getKeepById({ commit, dispatch }, keepId) {
      try {
        let res = await api.get("keeps/" + keepId)
        dispatch("editKeepsOnKeep", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    async editKeepsOnKeep({ commit, dispatch }, keep) {
      try {
        keep.keeps++
        console.log(keep)
        let res = await api.put("keeps/" + keep.id, keep)
        dispatch("getKeeps")
        commit("setActiveKeep", keep)
      } catch (error) {
        console.error(error);
      }
    },

    async getKeepsByVaultId({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId + "/keeps")
      } catch (error) {
        console.error(error);
      }
    },


  },
});
