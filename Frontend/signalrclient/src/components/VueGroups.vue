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
   
  <form>
    <input v-model="connectionId" placeholder="Paste user connection Id here"/>
    <input v-model="groupName" placeholder="Enter the group's name"/>
    <button @click="createNewGroup()" v-if="isCreateGroupInputValid">
      Create Group
    </button>
  </form>
  <br />

  <form>
    <input v-model="connectionId" placeholder="Enter user connection Id"/>
    <input v-model="message" placeholder="Enter message"/>
    <button @click="notifyUser()">
      Send Message to user
    </button>
  </form>

</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { getGroups, createGroup, leaveGroup, notifyUser, IAllGroups, IGroupManagement, IUserNotification } from '@/modules/axios/apiclient';
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
      groupName: '' as string,
      message: '' as string

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

      await createGroup(group);
    },

    async leaveGroup() {
      const group: IGroupManagement = {
        connectionId: this.connectionId,
        groupName: this.groupName
      };

      await leaveGroup(group);
    },
    async notifyUser() {
      const notification: IUserNotification = {
        userId: this.connectionId,
        message: this.message
      };
      await notifyUser(notification)
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
