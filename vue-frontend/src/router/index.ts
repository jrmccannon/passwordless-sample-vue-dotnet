import { createRouter, createWebHashHistory, RouteRecordRaw } from "vue-router";
import HomeView from "../views/HomeView.vue";
import { isAuthenticated } from "@/router/AuthenticationGuard";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  {
    path: "/about",
    name: "about",
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/AboutView.vue"),
  },
  {
    path: "/login",
    name: "login",
    component: () =>
      import(/* webpackChunkName: "login" */ "../views/LoginView.vue"),
  },
  {
    path: "/register",
    name: "register",
    component: () =>
      import(/* webpackChunkName: "register" */ "../views/RegisterView.vue"),
  },
  {
    path: "/user",
    name: "user",
    component: () =>
      import(/* webpackChunkName: "user" */ "../views/UserView.vue"),
    beforeEnter: isAuthenticated,
  },
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

export default router;
