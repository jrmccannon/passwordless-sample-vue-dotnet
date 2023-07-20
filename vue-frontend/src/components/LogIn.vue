<script setup lang="ts">
import { ref, computed } from "vue";
import { SignIn } from "@/services/SampleApiClient";
import { SignInUser } from "@/services/PasswordlessApiClient";
import { useAuthStore } from "@/store";
import router from "@/router";

const store = useAuthStore();

const username = ref("");

const loggedIn = computed(() => store.loggedIn);

const somethingWentWrong = ref(false);

async function login() {
  const { token, error } = await SignInUser(username.value);

  if (error) {
    console.error(error);
    return;
  }

  const response = await SignIn(token);

  if (response.loggedIn === true) {
    store.loggedIn = true;
    setTimeout(() => {
      router.push("/user");
    }, 1000);
  } else {
    console.error(response);
    somethingWentWrong.value = true;
  }
}
</script>

<template>
  <div>
    <label for="username">User Name: </label>
    <input id="username" type="text" v-model="username" />
    <button @click="login">Login</button>
  </div>
  <div>
    <h3 v-if="loggedIn">Successful login!</h3>
    <router-link to="/register">Register new user</router-link>
  </div>
</template>

<style scoped></style>
