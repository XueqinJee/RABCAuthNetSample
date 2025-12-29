import { service } from "@/utils/request";

const userApi = {
    async login(userName, password) {
        return await service.post('/auth/login', { username: userName, password: password })
    },
    async register(data) {
        return await service.post('/auth/register', data)
    },

    async sendCode(email){
        return await service.post('/sms/sendcode', { email: email})
    },
}

export { userApi }