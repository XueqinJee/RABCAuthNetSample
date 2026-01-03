<template>
    <div class="user-management">
        <el-card class="user-card">
            <template #header>
                <div class="card-header">
                    <span>用户管理</span>
                    <el-button type="primary" @click="handleAdd">新增用户</el-button>
                </div>
            </template>

            <art-table :colums="columns" :forms="formFields" :data="tableData" :loading="isLoading" :page="pageination"
                @on-search-handle="loadData" @on-page-size-change-handle="onPageSizeChange"
                @on-current-change-handle="onCurrentPageChange">
                <template #action="{ row, index }">
                    <el-button size="small" link @click="handleEdit(row)">编辑</el-button>
                    <el-button size="small" link type="warning" @click="handleResetPassword">修改密码</el-button>
                    <el-button size="small" link type="danger" @click="handleDelete">删除</el-button>
                </template>
            </art-table>
        </el-card>

        <UserDialog v-model:visible="dialogVisible" :is-edit="isEdit" :user-data="currentUser" @submit="onSubmit" />
        <ResetPasswordDialog v-model:visible="resetPasswordDialogVisible" :user-data="currentUser"
            @submit="handleResetPasswordSubmit" />
    </div>
</template>

<script>
export default {
    name: 'UserView'
}
</script>

<script setup>
import { ref, computed, watch } from 'vue'
import { ElMessage } from 'element-plus'
import UserDialog from './UserDialog.vue'
import ResetPasswordDialog from './ResetPasswordDialog.vue'

import ArtTable from '@/components/art-tables/ArtTable.vue'
import { useTable } from '@/components/art-tables/useTable'
import { userApi } from '@/api'

const dialogVisible = ref(false)
const isEdit = ref(false)
const currentUser = ref({})
const resetPasswordDialogVisible = ref(false)

const handleAdd = () => {
    isEdit.value = false
    currentUser.value = {}
    dialogVisible.value = true
}

const handleEdit = (row) => {
    isEdit.value = true
    currentUser.value = { ...row }
    dialogVisible.value = true
}

const handleDelete = (row) => {
    ElMessage.info(`删除用户: ${row.username}`)
}

const handleResetPassword = (row) => {
    currentUser.value = { ...row }
    resetPasswordDialogVisible.value = true
}

const handleResetPasswordSubmit = (formData) => {
    ElMessage.success(`用户 ${formData.username} 密码重置成功`)
    resetPasswordDialogVisible.value = false
}

const onSubmit = async (isEdit, data) => {
    isLoading.value = true
    try {
        if (isEdit) {
            await userApi.addUser(data)
        }else{
            await userApi.addUser(data)
        }
        await loadData()
    } finally {
        dialogVisible.value = false
        isLoading.value = false
    }

}

const {
    isLoading,
    tableData,
    onCurrentPageChange,
    onPageSizeChange,
    loadData,
    columns,
    formFields,
    pageination
} = useTable({
    core: {
        apiFn: userApi.getUserData,
        columnsFactory: () => [
            { prop: 'id', label: '编号', type: 'text', width: 80 },
            { prop: 'nickName', label: '昵称', type: 'text', width: 200 },
            { prop: 'userName', label: '登录名', type: 'text', width: 120 },
            { prop: 'email', label: '邮箱', type: 'text', width: 200 },
            { prop: 'role', label: '角色', type: 'text', width: 200 },
            { prop: 'status', label: '状态', type: 'text', width: 200 },
            { prop: 'createOn', label: '建立时间', type: 'date', width: 200 },
            { prop: 'action', label: '操作', useSlot: true }
        ],
        formFactory: () => [
            { name: 'nickName', label: '昵称', type: 'input' },
            { name: 'userName', label: '用户名', type: 'input' },
            { name: 'email', label: '邮箱', type: 'input' },
            { name: 'email', label: '角色', type: 'select' },
            { name: 'email', label: '状态', type: 'select' },
        ]
    }
})

</script>

<style scoped>
.user-management {
    height: 100%;
    padding: 8px;
}

.user-management :deep(.el-card) {
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
    height: 100%;
}

.user-management :deep(.user-card) {
    display: flex;
    flex-direction: column;
}

.user-management :deep(.el-card__body) {
    flex: 1;
    display: flex;
    flex-direction: column;
    padding: 12px;
    overflow: hidden;
}

.card-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.content-wrapper {
    flex: 1;
    display: flex;
    flex-direction: column;
    overflow: hidden;
}

.search-form {
    flex-shrink: 0;
    padding: 12px;
    background: #f8f9fa;
    border-radius: 4px;
    margin-bottom: 12px;
}

.search-form-content {
    margin: 0;
}

.table-wrapper {
    flex: 1;
    display: flex;
    flex-direction: column;
    overflow: hidden;
    min-height: 0;
}

.pagination-container {
    margin-top: 12px;
    display: flex;
    justify-content: flex-start;
    flex-shrink: 0;
}
</style>
