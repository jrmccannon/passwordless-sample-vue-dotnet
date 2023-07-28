<script setup lang="ts">
import { ref, onMounted } from "vue";
import Penguin from "@/models/Penguin";
import { GetPenguins } from "@/services/SampleApiClient";
import { useAuthStore } from "@/store";

const store = useAuthStore();

const penguins = ref<Array<Penguin>>([]);

onMounted(async () => {
  penguins.value = await GetPenguins(store.userToken);
});
</script>

<template>
  <div v-for="(penguin, index) in penguins" :key="index">
    <p>
      Name: {{ penguin.name }} Scientific Name: {{ penguin.scientificName }}
    </p>
    <ul>
      Favorite Foods
      <template v-for="(food, index) in penguin.favoriteFoods" :key="index">
        <li>{{ food }}</li>
      </template>
    </ul>
  </div>
</template>

<style scoped></style>
