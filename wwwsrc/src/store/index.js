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
    activeVault: {},
    currentVaultsKeeps: [],
    vaultKeepKeepId: null,
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

    setActiveVault(state, vault) {
      state.activeVault = vault
    },

    setUserVaults(state, vaults) {
      state.userVaults = vaults;
    },

    sendKeepId(state, keepId) {
      state.vaultKeepKeepId = keepId
    },

    setVaultKeeps(state, vaultKeeps) {
      state.currentVaultsKeeps = vaultKeeps
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
        let res = await api.put("keeps/" + viewedKeep.id + "/views", viewedKeep)
        dispatch("setActiveKeep", res.data)
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

    async createVault({ commit, dispatch }, vault) {
      try {
        let res = await api.post("vaults", vault.newVault)
        console.log(res)
        dispatch("getProfileVaults", vault.profileId)
        dispatch("getUserVaults", vault.profileId)
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

    setActiveVault({ commit, dispatch }, vault) {
      commit("setActiveVault", vault)
    },

    //THIS IS CALLED ON APP, GETS THE USERS PERSONALIZED VAULTS
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
        console.log(res)
        commit("setVaultKeeps", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    async getActiveVault({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId)
        commit("setActiveVault", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    async deleteVault({ commit, dispatch }, vault) {
      try {
        let res = await api.delete("vaults/" + vault.vaultId)
        dispatch("getProfileVaults", vault.profileId)
      } catch (error) {
        console.error();
      }
    },

    async removeKeep({ commit, dispatch }, vaultKeep) {
      try {
        let res = await api.delete("vaultkeeps/" + vaultKeep.vaultKeepId)
        dispatch("getKeepsByVaultId", vaultKeep.vaultId)
      } catch (error) {
        console.error(error);
      }
    }
  },
});
