import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import { VueSignalR } from '@quangdao/vue-signalr';

createApp(App)
.use(store)
.use(router)
.use(VueSignalR, { url: 'http://localhost:5142/CommunicationHub' })
.mount('#app')
