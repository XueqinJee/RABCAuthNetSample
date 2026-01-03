import { service } from "@/utils/request";

const roleApi = {
    async getRoleData(params) {
        return await service.get('/role/pages', { params: params })
    },

    async addRole(data){
        return await service.post('/role/add', data)
    },

    async editRole(data){
        return await service.post('/role/update', data)
    },

    async deleteRole(id){
        return await service.delete(`/role/delete/${id}`)
    },

    async getRoleList(keyword = '') {
        return await service.get('/role/list', { 
            params: { keyword } 
        })
    }
}

export { roleApi }