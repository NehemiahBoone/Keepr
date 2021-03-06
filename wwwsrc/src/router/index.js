import Vue from "vue";
import VueRouter from "vue-router";
// @ts-ignore
import Home from "../pages/Home.vue";
import Profile from "../pages/ProfilePage.vue";
import Vault from "../pages/VaultPage.vue";
import { authGuard } from "@bcwdev/auth0-vue"

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/profile/:id",
    name: "Profile",
    component: Profile,
  },
  {
    path: "/profile/:profileId/vaults/:vaultId",
    name: "Vault",
    component: Vault,
    beforeEnter: authGuard,
  }
];

const router = new VueRouter({
  routes,
});

export default router;
