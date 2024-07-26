import axios from 'axios';

// 創建 axios 實例
const instance = axios.create({
    baseURL: 'https://localhost:5001'
});

// 添加請求攔截器
instance.interceptors.request.use(config => {
    const token = localStorage.getItem('authToken');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});

export default instance;
