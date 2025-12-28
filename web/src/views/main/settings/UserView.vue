<template>
  <div class="user-management">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>用户管理</span>
          <el-button type="primary" @click="handleAdd">新增用户</el-button>
        </div>
      </template>
      <el-table :data="tableData" style="width: 100%">
        <el-table-column prop="id" label="ID" width="80" />
        <el-table-column prop="username" label="用户名" width="150" />
        <el-table-column prop="nickname" label="昵称" width="150" />
        <el-table-column prop="email" label="邮箱" />
        <el-table-column prop="role" label="角色" width="120" />
        <el-table-column prop="status" label="状态" width="100">
          <template #default="{ row }">
            <el-tag :type="row.status === '启用' ? 'success' : 'danger'">{{ row.status }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="200">
          <template #default="{ row }">
            <el-button link type="primary" @click="handleEdit(row)">编辑</el-button>
            <el-button link type="danger" @click="handleDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>
  </div>
</template>

<script>
export default {
  name: 'UserView'
}
</script>

<script setup>
import { ref } from 'vue'
import { ElMessage } from 'element-plus'

const tableData = ref([
  { id: 1, username: 'admin', nickname: '管理员', email: 'admin@example.com', role: '超级管理员', status: '启用' },
  { id: 2, username: 'user1', nickname: '用户1', email: 'user1@example.com', role: '普通用户', status: '启用' },
  { id: 3, username: 'user2', nickname: '用户2', email: 'user2@example.com', role: '普通用户', status: '禁用' },
])

const handleAdd = () => {
  ElMessage.info('新增用户')
}

const handleEdit = (row) => {
  ElMessage.info(`编辑用户: ${row.username}`)
}

const handleDelete = (row) => {
  ElMessage.info(`删除用户: ${row.username}`)
}
</script>

<style scoped>
.user-management {
  height: 100%;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
