<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <VueGroups msg="Welcome to Your Vue.js + TypeScript App"/>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import VueGroups from '@/components/VueGroups.vue'; // @ is an alias to /src

export default defineComponent({
  name: 'HomeView',
  components: {
    VueGroups,
  },
});
</script>
