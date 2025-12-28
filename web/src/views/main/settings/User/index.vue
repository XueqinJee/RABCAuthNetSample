<template>
  <div class="user-management">
    <el-card class="user-card">
      <template #header>
        <div class="card-header">
          <span>用户管理</span>
          <el-button type="primary" @click="handleAdd">新增用户</el-button>
        </div>
      </template>
      
      <div class="content-wrapper">
        <div class="search-form">
          <el-form :inline="true" :model="searchForm" class="search-form-content">
            <el-form-item label="用户名">
              <el-input v-model="searchForm.username" placeholder="请输入用户名" clearable />
            </el-form-item>
            <el-form-item label="昵称">
              <el-input v-model="searchForm.nickname" placeholder="请输入昵称" clearable />
            </el-form-item>
            <el-form-item label="邮箱">
              <el-input v-model="searchForm.email" placeholder="请输入邮箱" clearable />
            </el-form-item>
            <el-form-item label="角色">
              <el-select v-model="searchForm.role" placeholder="请选择角色" clearable style="width: 150px">
                <el-option label="超级管理员" value="超级管理员" />
                <el-option label="普通用户" value="普通用户" />
                <el-option label="访客" value="访客" />
              </el-select>
            </el-form-item>
            <el-form-item label="状态">
              <el-select v-model="searchForm.status" placeholder="请选择状态" clearable style="width: 120px">
                <el-option label="启用" value="启用" />
                <el-option label="禁用" value="禁用" />
              </el-select>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="handleSearch">搜索</el-button>
              <el-button @click="handleReset">重置</el-button>
            </el-form-item>
          </el-form>
        </div>

        <div class="table-wrapper">
          <el-table :data="tableData" style="width: 100%" height="100%">
            <el-table-column prop="id" label="ID" width="80" />
            <el-table-column prop="username" label="用户名" width="150" />
            <el-table-column prop="nickname" label="昵称" width="150" />
            <el-table-column prop="email" label="邮箱" min-width="200" />
            <el-table-column prop="role" label="角色" width="120" />
            <el-table-column prop="status" label="状态" width="100">
              <template #default="{ row }">
                <el-tag :type="row.status === '启用' ? 'success' : 'danger'">{{ row.status }}</el-tag>
              </template>
            </el-table-column>
            <el-table-column label="操作" width="280">
              <template #default="{ row }">
                <el-button link type="primary" @click="handleEdit(row)">编辑</el-button>
                <el-button link type="warning" @click="handleResetPassword(row)">重置密码</el-button>
                <el-button link type="danger" @click="handleDelete(row)">删除</el-button>
              </template>
            </el-table-column>
          </el-table>

          <div class="pagination-container">
            <el-pagination
              v-model:current-page="pagination.currentPage"
              v-model:page-size="pagination.pageSize"
              :page-sizes="[10, 20, 50, 100]"
              :total="pagination.total"
              layout="total, sizes, prev, pager, next, jumper"
              @size-change="handleSizeChange"
              @current-change="handleCurrentChange"
            />
          </div>
        </div>
      </div>
    </el-card>

    <UserDialog
      v-model:visible="dialogVisible"
      :is-edit="isEdit"
      :user-data="currentUser"
      @submit="handleSubmit"
    />
    <ResetPasswordDialog
      v-model:visible="resetPasswordDialogVisible"
      :user-data="currentUser"
      @submit="handleResetPasswordSubmit"
    />
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

const allData = ref([
  { id: 1, username: 'admin', nickname: '管理员', email: 'admin@example.com', role: '超级管理员', status: '启用' },
  { id: 2, username: 'user1', nickname: '用户1', email: 'user1@example.com', role: '普通用户', status: '启用' },
  { id: 3, username: 'user2', nickname: '用户2', email: 'user2@example.com', role: '普通用户', status: '禁用' },
  { id: 4, username: 'user3', nickname: '用户3', email: 'user3@example.com', role: '访客', status: '启用' },
  { id: 5, username: 'user4', nickname: '用户4', email: 'user4@example.com', role: '普通用户', status: '启用' },
  { id: 6, username: 'user5', nickname: '用户5', email: 'user5@example.com', role: '超级管理员', status: '禁用' },
  { id: 7, username: 'user6', nickname: '用户6', email: 'user6@example.com', role: '普通用户', status: '启用' },
  { id: 8, username: 'user7', nickname: '用户7', email: 'user7@example.com', role: '访客', status: '禁用' },
  { id: 9, username: 'user8', nickname: '用户8', email: 'user8@example.com', role: '普通用户', status: '启用' },
  { id: 10, username: 'user9', nickname: '用户9', email: 'user9@example.com', role: '普通用户', status: '启用' },
  { id: 11, username: 'user10', nickname: '用户10', email: 'user10@example.com', role: '超级管理员', status: '启用' },
  { id: 12, username: 'user11', nickname: '用户11', email: 'user11@example.com', role: '普通用户', status: '禁用' },
])

const searchForm = ref({
  username: '',
  nickname: '',
  email: '',
  role: '',
  status: ''
})

const pagination = ref({
  currentPage: 1,
  pageSize: 10,
  total: 0
})

const filteredData = computed(() => {
  return allData.value.filter(item => {
    const matchUsername = !searchForm.value.username || item.username.toLowerCase().includes(searchForm.value.username.toLowerCase())
    const matchNickname = !searchForm.value.nickname || item.nickname.toLowerCase().includes(searchForm.value.nickname.toLowerCase())
    const matchEmail = !searchForm.value.email || item.email.toLowerCase().includes(searchForm.value.email.toLowerCase())
    const matchRole = !searchForm.value.role || item.role === searchForm.value.role
    const matchStatus = !searchForm.value.status || item.status === searchForm.value.status
    return matchUsername && matchNickname && matchEmail && matchRole && matchStatus
  })
})

watch(searchForm, () => {
  pagination.value.currentPage = 1
}, { deep: true })

const tableData = computed(() => {
  pagination.value.total = filteredData.value.length
  const start = (pagination.value.currentPage - 1) * pagination.value.pageSize
  const end = start + pagination.value.pageSize
  return filteredData.value.slice(start, end)
})

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

const handleReset = () => {
  searchForm.value = {
    username: '',
    nickname: '',
    email: '',
    role: '',
    status: ''
  }
}

const handleSearch = () => {
  pagination.value.currentPage = 1
}

const handleSubmit = (formData) => {
  if (isEdit.value) {
    const index = allData.value.findIndex(item => item.id === formData.id)
    if (index > -1) {
      allData.value[index] = {
        ...formData,
        password: undefined
      }
      ElMessage.success('编辑成功')
    }
  } else {
    const newId = Math.max(...allData.value.map(item => item.id)) + 1
    allData.value.push({
      ...formData,
      id: newId,
      password: undefined
    })
    ElMessage.success('新增成功')
  }
  dialogVisible.value = false
}

const handleSizeChange = (val) => {
  pagination.value.pageSize = val
  pagination.value.currentPage = 1
}

const handleCurrentChange = (val) => {
  pagination.value.currentPage = val
}
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
