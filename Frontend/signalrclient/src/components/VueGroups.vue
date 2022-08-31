<template>
  <div class="groupsList">
    <button  @click="getAllGroups()">
        Get all groups
    </button >
    <div v-for="(group, i) in this.allGroups" :key = i>
      {{group}}
    </div>
  </div>
  <br />
   <!-- @submit="validate" -->
  <form class="createGroup">
    <input v-model="connectionId" placeholder="Paste user connection Id here"/>
    <input v-model="groupName" placeholder="Enter the group's name"/>
     <!-- v-if="isCreateGroupInputValid" -->
    <button @click="createNewGroup()">
      Create Group
    </button>
  </form>
  <br />

</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { getGroups, createGroup, leaveGroup, IAllGroups, IGroupManagement } from '@/modules/axios/apiclient';
import axios, { AxiosError, AxiosResponse } from 'axios';

export default defineComponent({
  name: 'HelloWorld',
  props: {
    msg: String,
  },
  data() {
    return {
      allGroups: [] as string[],      
      connectionId: '' as string,
      groupName: '' as string

    }
  },
  computed: {
    isCreateGroupInputValid(): boolean {
      console.log()
      if(this.connectionId && this.connectionId.trim() && this.groupName && this.groupName.trim())
        return true;
      else
        return false;
    }
  },
  methods: {
    async getAllGroups() {
      const allGroupsObject: IAllGroups = await getGroups();
      this.allGroups = allGroupsObject.allGroups;
    },

    async createNewGroup() {
      const group: IGroupManagement = {
        connectionId: this.connectionId,
        groupName: this.groupName
      };
      // axios.defaults.baseURL = 'http://localhost:5142';
      // await axios.post('/groups/creategroup', group)
      // .then((response) =>{
      //   console.log(response)
      // })
      // .catch((error) =>{
      //   console.log(error)
      // });
      await createGroup(group);
    },

    async leaveGroup() {
      const group: IGroupManagement = {
        connectionId: this.connectionId,
        groupName: this.groupName
      };

      await leaveGroup(group);
    }
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
