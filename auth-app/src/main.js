import { createApp } from 'vue';
import App from './App.vue';
import axios from 'axios';
import router from './router'; // 導入 router 配置


// 設定全局的 axios 默認值
//axios.defaults.baseURL = 'https://api.example.com';
//axios.defaults.headers.common['Authorization'] = 'Bearer YOUR_TOKEN';
const app = createApp(App);
app.config.productionTip = false;
// 全局註冊 axios
app.config.globalProperties.$axios = axios;
// 使用 Vue Router 插件
app.use(router);

// 挂載應用
app.mount('#app');
// new Vue({
//     router, // 將 router 注入到 Vue 實例中
//     render: h => h(App),
// }).$mount('#app');
