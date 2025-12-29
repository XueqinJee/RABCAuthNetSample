import axios from "axios";
import { ElMessage } from "element-plus";

const service = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    timeout: import.meta.env.VITE_REQUEST_TIMEOUT || 10000,
    headers: {
        'Content-Type': 'application/json;chatset=utf-8'
    }
})

// 请求拦截
service.interceptors.request.use(
    config => {
        const token = window.localStorage.getItem("token")
        if (token) {
            config.headers.Authorization = `Bearer ${token}`
        }
        return config;
    },
    error => {
        console.error("Request Error:", error)
        return Promise.reject(error)
    }
)

// 响应拦截
service.interceptors.response.use(
    response => {
        const result = response.data
        if (response.status != 200) {
            ElMessage.error(result.message || 'Request failed')

            if (result.code === 401) {
                window.location.href = '/login'
            }
            return Promise.reject(result)
        }
        return result;
    },
    error => {
        console.error("Response Error:", error)
        const data = error.response.data
        console.log(data);
        
        if (data.message || data.msg) {
            ElMessage.error(data.message || data.msg)
        } else {
            ElMessage.error(error.message || error.msg || 'Network error')
        }
        return Promise.reject(error)
    }
)

export { service }