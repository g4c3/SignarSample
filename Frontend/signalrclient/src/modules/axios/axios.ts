import axios, { AxiosStatic } from 'axios';
import { App } from 'vue';
import VueAxios from 'vue-axios';


export default {
    install:(app: App) => {
        function configure(): AxiosStatic {
            axios.defaults.timeout = 5000;
            axios.defaults.headers.common.Accept = 'application/json';
            axios.defaults.headers.common.ContentType = 'application/json';
            axios.defaults.baseURL = 'http://localhost:5142';

            return axios;
        }
        const ax = configure();
        app.use(VueAxios, ax);
    }
}