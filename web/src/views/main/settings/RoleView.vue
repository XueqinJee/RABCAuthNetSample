<template>
  <div class="role-management">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>角色管理</span>
          <el-button type="primary" @click="handleAdd">新增角色</el-button>
        </div>
      </template>
      <el-table :data="tableData" style="width: 100%">
        <el-table-column prop="id" label="ID" width="80" />
        <el-table-column prop="name" label="角色名称" width="150" />
        <el-table-column prop="code" label="角色代码" width="150" />
        <el-table-column prop="description" label="描述" />
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
  name: 'RoleView'
}
</script>

<script setup>
import { ref } from 'vue'
import { ElMessage } from 'element-plus'

const tableData = ref([
  { id: 1, name: '超级管理员', code: 'super_admin', description: '拥有所有权限', status: '启用' },
  { id: 2, name: '普通用户', code: 'user', description: '普通用户权限', status: '启用' },
  { id: 3, name: '访客', code: 'guest', description: '访客权限', status: '启用' },
])

const handleAdd = () => {
  ElMessage.info('新增角色')
}

const handleEdit = (row) => {
  ElMessage.info(`编辑角色: ${row.name}`)
}

const handleDelete = (row) => {
  ElMessage.info(`删除角色: ${row.name}`)
}
</script>

<style scoped>
.role-management {
  height: 100%;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
