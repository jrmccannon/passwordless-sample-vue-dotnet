<script setup lang="ts">
import { ref } from "vue";
import router from "@/router";
import {
  ErrorRegistrationResponse,
  Register,
} from "@/services/SampleApiClient";
import { RegisterUser } from "@/services/PasswordlessApiClient";

const username = ref("");

const registered = ref(false);
const errorMsg = ref("");

async function register() {
  const response = await Register(username.value);

  if (response instanceof ErrorRegistrationResponse) {
    errorMsg.value = response.errorMsg;
    return;
  }

  const { token, error } = await RegisterUser(response.token);

  if (token) {
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
  <div v-if="!registered">
    <label for="username">User Name: </label>
    <input id="username" type="text" v-model="username" />
    <button @click="register">Register</button>
  </div>
  <p v-if="registered" class="success">Successfully registered!</p>
  <p v-if="errorMsg" class="error">{{ errorMsg }}</p>
</template>

<style>
.success {
  color: green;
}

.error {
  color: red;
}
</style>
