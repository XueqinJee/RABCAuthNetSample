import { service } from "@/utils/request";

const userApi = {
    async login(userName, password) {
        return await service.post('/auth/login', { username: userName, password: password })
    },
    async register(data) {
        return await service.post('/auth/register', data)
    }
}

export { userApi }