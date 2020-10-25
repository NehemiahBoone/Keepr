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
    keeps: []
  },

  mutations: {
    setProfile(state, profile) {
      state.profile = profile;
    },

    setViewedProfile(state, profile){
      state.viewedProfile = profile;
    },

    setKeeps(state, keeps){
      state.keeps = keeps;
    },

    setProfileKeeps(state, keeps){
      state.profileKeeps = keeps
    },

    setProfileVaults(state, vaults){
      state.profileVaults = vaults
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

    async getKeeps({commit, dispatch}){
      try {
        let res = await api.get("keeps")
        console.log(res)
        commit("setKeeps", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    async getSearchedProfile({commit,dispatch}, profileId){
      try {
        let res = await api.get("profiles/" + profileId)
        console.log(res)
        commit("setViewedProfile", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    async getProfileKeeps({commit,dispatch}, profileId){
      try {
        let res = await api.get("profiles/" + profileId + "/keeps")
        commit("setProfileKeeps", res.data)
      } catch (error) {
        console.error(error);
      }
    },
    
    async getProfileVaults({commit,dispatch}, profileId){
      try {
        let res = await api.get("profiles/" + profileId + "/vaults")
        commit("setProfileVaults", res.data)
      } catch (error) {
        console.error(error);
      }
    }
  },
});
