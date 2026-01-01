import { ref, computed, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { defineStore } from 'pinia'
import { userApi } from '@/api'
import { ElMessage } from 'element-plus'

import CryptoJS from 'crypto-js'
const SECRET_KEY = 'wadjfsfesfrg-secret-key-123'

export const useUserStore = defineStore('user', () => {
    const userInfo = ref({
        token: {
            token: '',
            refreshToken: '',
            userName: '',
            nickName: '',
            expireTime: ''
        }
    })

    const router = useRouter()

    // 判断是否已登录
    const isLogin = computed(() => {
        return userInfo.value.token.token && !isTokenExpired.value
    })

    // 判断TOken是否已过期
    const isTokenExpired = computed(() => {
        if (!userInfo.value.token.expireTime) return true;
        const expireTime = typeof userInfo.value.token.expireTime === 'string' ? new Date(userInfo.value.token.expireTime) : userInfo.value.token.expireTime
        return Date.now() > expireTime
    })


    /**
     * 登录
     * @param {*} loginInfo 
     * @returns 
     */
    const loginUserAsync = async (userName, password) => {
        try {
            if (!userName || !password) {
                ElMessage.error("用户名和密码不能为空")
                return false;
            }

            var login = await userApi.login(userName, password)
            if (login.code != 200) {
                ElMessage.error(login.msg || '登录失败')
                return false;
            }

            // 登录信息
            userInfo.value.token = { ...login.data }

            ElMessage.success("登录成功")
            return true;
        } catch (ex) {
            console.error('登录异常：', ex)
            ElMessage.error('登录异常，请稍后重试')
            return false;
        }
    }

    /**
     * 登出
     */
    const logout = () => {
        userInfo.value.token = {
            token: '',
            refreshToken: '',
            userName: '',
            nickName: '',
            expireTime: ''
        }

        router.push('/login')
        ElMessage.success("已成功退出登录")
    }

    return { userInfo, loginUserAsync, logout, isLogin }
}, {
    persist: {
        key: 'user-token',
        storage: localStorage,
        paths: ['userInfo'],
        serializer: {
            serialize: (state) => {
                return CryptoJS.AES.encrypt(JSON.stringify(state), SECRET_KEY).toString()
            },
            deserialize: (encrypted) => {
                const bytes = CryptoJS.AES.decrypt(encrypted, SECRET_KEY)
                return JSON.parse(bytes.toString(CryptoJS.enc.Utf8))
            }
        }
    }
})