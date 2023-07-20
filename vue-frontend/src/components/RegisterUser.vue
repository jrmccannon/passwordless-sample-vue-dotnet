<script setup lang="ts">
import { ref } from "vue";
import router from "@/router";
import { Register } from "@/services/SampleApiClient";
import { RegisterUser } from "@/services/PasswordlessApiClient";

const username = ref("");

const registered = ref(false);
const somethingWentWrong = ref(false);

async function register() {
  const registerToken = await Register(username.value);

  if (registerToken.errors) {
    console.error("registertoken", registerToken);
    return;
  }

  const { token, error } = await RegisterUser(registerToken.token);
  if (token) {
    console.log("registration success");
    setTimeout(() => {
      router.push("/login");
    }, 1000);

    registered.value = true;
  } else {
    console.error(error);
  }
}
</script>

<template>
  <div if="!registered">
    <label for="username">User Name: </label>
    <input id="username" type="text" v-model="username" />
    <button @click="register">Register</button>
  </div>
  <p v-if="registered" class="success">Successfully registered!</p>
  <p v-if="somethingWentWrong" class="error">An error occured.</p>
</template>

<style>
.success {
  color: green;
}

.error {
  color: red;
}
</style>
