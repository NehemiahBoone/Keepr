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
    keeps: []
  },

  mutations: {
    setProfile(state, profile) {
      state.profile = profile;
    },

    setKeeps(state, keeps){
      state.keeps = keeps;
    },

    setViewedProfile(state, profile){
      state.viewedProfile = profile;
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
        commit("setViewedProfile", res.data)
      } catch (error) {
        console.error(error);
      }
    },

    async getProfileKeeps({commit,dispatch}, profileId){
      try {
        let res = await api.get("profiles/" + profileId + "/keeps")
        console.log(res)
      } catch (error) {
        console.error(error);
      }
    },
  },
});
