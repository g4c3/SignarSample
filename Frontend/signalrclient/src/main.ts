import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import { VueSignalR } from '@quangdao/vue-signalr';
import axios from './modules/axios/axios';

const app = createApp(App);
app
    .use(store)
    .use(router)
    .use(VueSignalR, { url: 'http://localhost:5142/CommunicationHub' })
    .use(axios);

app.mount('#app')
