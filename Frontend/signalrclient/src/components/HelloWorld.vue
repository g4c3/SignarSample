<template>
  <div class="hello">
    <button  @click="getAllGroups()">
        Get all groups
        {{this.allGroups}}
    </button >
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useSignalR } from '@quangdao/vue-signalr';
import { getGroups, AllGroups } from '@/modules/axios/apiclient';

export default defineComponent({
  name: 'HelloWorld',
  props: {
    msg: String
  },
  data() {
    return {
      allGroups: {} as AllGroups
    }
  },
  methods: {
    async getAllGroups() {
      this.allGroups = await getGroups();
    }

  },
  setup() {
    const signalr = useSignalR();
    console.log("starting connection");
  }
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
