<script setup lang="ts">
import { ref } from "vue";
import router from "@/router";
import {
  ErrorRegistrationResponse,
  Register,
} from "@/services/SampleApiClient";
import { RegisterUser } from "@/services/PasswordlessApiClient";

const username = ref("");
const nickname = ref("");

const registered = ref(false);
const errorMsg = ref("");

async function register() {
  const response = await Register(username.value);

  if (response instanceof ErrorRegistrationResponse) {
    errorMsg.value = response.errorMsg;
    return;
  }

  const { token, error } = await RegisterUser(response.token, nickname.value);

  if (token) {
    setTimeout(() => {
      router.push("/login");
    }, 1000);

    registered.value = true;
  } else {
    errorMsg.value = error?.title ?? "Unknown occurred saving passkey.";
  }
}
</script>

<template>
  <div v-if="!registered">
    <label for="username">User Name: </label>
    <input id="username" type="text" v-model="username" />
    <label for="nickname">Nickname: </label>
    <input id="nickname" type="text" v-model="nickname" />
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
