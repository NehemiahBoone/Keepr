import Vue from "vue";
import VueRouter from "vue-router";
// @ts-ignore
import Home from "../pages/Home.vue";
import Profile from "../pages/ProfilePage.vue";

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
  }
];

const router = new VueRouter({
  routes,
});

export default router;
