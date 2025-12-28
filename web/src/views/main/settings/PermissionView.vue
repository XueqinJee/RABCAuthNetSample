<template>
  <div class="permission-management">
    <el-card>
      <template #header>
        <div class="card-header">
          <span>权限管理</span>
          <el-button type="primary" @click="handleAdd">新增权限</el-button>
        </div>
      </template>
      <el-table :data="tableData" style="width: 100%">
        <el-table-column prop="id" label="ID" width="80" />
        <el-table-column prop="name" label="权限名称" width="150" />
        <el-table-column prop="code" label="权限代码" width="200" />
        <el-table-column prop="type" label="类型" width="100">
          <template #default="{ row }">
            <el-tag :type="row.type === '菜单' ? 'primary' : 'success'">{{ row.type }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="description" label="描述" />
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
  name: 'PermissionView'
}
</script>

<script setup>
import { ref } from 'vue'
import { ElMessage } from 'element-plus'

const tableData = ref([
  { id: 1, name: '用户管理', code: 'user:manage', type: '菜单', description: '用户管理权限' },
  { id: 2, name: '角色管理', code: 'role:manage', type: '菜单', description: '角色管理权限' },
  { id: 3, name: '权限管理', code: 'permission:manage', type: '菜单', description: '权限管理权限' },
  { id: 4, name: '用户添加', code: 'user:add', type: '按钮', description: '添加用户权限' },
  { id: 5, name: '用户删除', code: 'user:delete', type: '按钮', description: '删除用户权限' },
])

const handleAdd = () => {
  ElMessage.info('新增权限')
}

const handleEdit = (row) => {
  ElMessage.info(`编辑权限: ${row.name}`)
}

const handleDelete = (row) => {
  ElMessage.info(`删除权限: ${row.name}`)
}
</script>

<style scoped>
.permission-management {
  height: 100%;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
