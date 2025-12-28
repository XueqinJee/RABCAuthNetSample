<template>
  <div class="menu-management">
    <el-card class="menu-card">
      <template #header>
        <div class="card-header">
          <span>菜单管理</span>
          <div>
            <el-button type="primary" @click="handleScanMenus">扫描菜单</el-button>
            <el-button type="success" @click="handleAdd">新增菜单</el-button>
          </div>
        </div>
      </template>
      
      <div class="content-wrapper">
        <div class="table-wrapper">
          <el-table
            :data="tableData"
            style="width: 100%"
            height="100%"
            row-key="id"
            :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
            default-expand-all
          >
            <el-table-column prop="name" label="菜单名称" width="250" />
            <el-table-column prop="type" label="类型" width="100">
              <template #default="{ row }">
                <el-tag :type="getTypeTagType(row.type)">{{ getTypeLabel(row.type) }}</el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="path" label="路径" min-width="200" />
            <el-table-column prop="component" label="组件" min-width="200" />
            <el-table-column prop="icon" label="图标" width="100" />
            <el-table-column prop="sort" label="排序" width="80" />
            <el-table-column prop="status" label="状态" width="80">
              <template #default="{ row }">
                <el-tag :type="row.status === '启用' ? 'success' : 'danger'">{{ row.status }}</el-tag>
              </template>
            </el-table-column>
            <el-table-column label="操作" width="280" fixed="right">
              <template #default="{ row }">
                <el-button link type="primary" @click="handleAdd(row)" v-if="row.type !== '按钮'">新增子菜单</el-button>
                <el-button link type="primary" @click="handleEdit(row)">编辑</el-button>
                <el-button link type="danger" @click="handleDelete(row)">删除</el-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
      </div>
    </el-card>

    <MenuDialog
      v-model:visible="dialogVisible"
      :is-edit="isEdit"
      :menu-data="currentMenu"
      :menu-options="menuOptions"
      @submit="handleSubmit"
    />
  </div>
</template>

<script>
export default {
  name: 'MenuView'
}
</script>

<script setup>
import { ref, computed } from 'vue'
import { ElMessage } from 'element-plus'
import MenuDialog from './MenuDialog.vue'
import { scanComponents } from '@/utils/componentScanner.js'

const allData = ref([
  {
    id: 1,
    name: '系统管理',
    type: '目录',
    path: '/system',
    component: '',
    icon: 'Setting',
    sort: 1,
    status: '启用',
    children: [
      {
        id: 2,
        name: '用户管理',
        type: '菜单',
        path: '/system/user',
        component: 'main/settings/User/index',
        icon: 'User',
        sort: 1,
        status: '启用',
        children: [
          { id: 21, name: '新增', type: '按钮', path: '', component: '', icon: '', sort: 1, status: '启用' },
          { id: 22, name: '编辑', type: '按钮', path: '', component: '', icon: '', sort: 2, status: '启用' },
          { id: 23, name: '删除', type: '按钮', path: '', component: '', icon: '', sort: 3, status: '启用' }
        ]
      },
      {
        id: 3,
        name: '角色管理',
        type: '菜单',
        path: '/system/role',
        component: 'main/settings/RoleView',
        icon: 'Lock',
        sort: 2,
        status: '启用',
        children: [
          { id: 31, name: '新增', type: '按钮', path: '', component: '', icon: '', sort: 1, status: '启用' },
          { id: 32, name: '编辑', type: '按钮', path: '', component: '', icon: '', sort: 2, status: '启用' },
          { id: 33, name: '删除', type: '按钮', path: '', component: '', icon: '', sort: 3, status: '启用' }
        ]
      },
      {
        id: 4,
        name: '权限管理',
        type: '菜单',
        path: '/system/permission',
        component: 'main/settings/PermissionView',
        icon: 'Key',
        sort: 3,
        status: '启用',
        children: [
          { id: 41, name: '新增', type: '按钮', path: '', component: '', icon: '', sort: 1, status: '启用' },
          { id: 42, name: '编辑', type: '按钮', path: '', component: '', icon: '', sort: 2, status: '启用' },
          { id: 43, name: '删除', type: '按钮', path: '', component: '', icon: '', sort: 3, status: '启用' }
        ]
      },
      {
        id: 5,
        name: '菜单管理',
        type: '菜单',
        path: '/system/menu',
        component: 'main/settings/Menu/index',
        icon: 'Menu',
        sort: 4,
        status: '启用',
        children: [
          { id: 51, name: '新增', type: '按钮', path: '', component: '', icon: '', sort: 1, status: '启用' },
          { id: 52, name: '编辑', type: '按钮', path: '', component: '', icon: '', sort: 2, status: '启用' },
          { id: 53, name: '删除', type: '按钮', path: '', component: '', icon: '', sort: 3, status: '启用' }
        ]
      }
    ]
  }
])

const tableData = ref([])
const dialogVisible = ref(false)
const isEdit = ref(false)
const currentMenu = ref({})
const menuOptions = ref([])

const flattenData = (data, parentId = null) => {
  const result = []
  data.forEach(item => {
    const flatItem = { ...item, parentId }
    result.push(flatItem)
    if (item.children && item.children.length > 0) {
      result.push(...flattenData(item.children, item.id))
    }
  })
  return result
}

const buildTree = (flatData, parentId = null) => {
  return flatData
    .filter(item => item.parentId === parentId)
    .map(item => ({
      ...item,
      children: buildTree(flatData, item.id)
    }))
}

const getTypeTagType = (type) => {
  const typeMap = {
    '目录': 'primary',
    '菜单': 'success',
    '按钮': 'warning'
  }
  return typeMap[type] || ''
}

const getTypeLabel = (type) => {
  return type
}

const loadMenuOptions = () => {
  const flatData = flattenData(allData.value)
  menuOptions.value = flatData.filter(item => item.type !== '按钮').map(item => ({
    label: item.name,
    value: item.id,
    type: item.type
  }))
}

const loadData = () => {
  tableData.value = JSON.parse(JSON.stringify(allData.value))
  loadMenuOptions()
}

const handleScanMenus = async () => {
  try {
    ElMessage.info('正在扫描组件...')
    const components = await scanComponents()
    
    ElMessage.success(`扫描完成，共找到 ${components.length} 个组件`)
    
    console.log('扫描到的组件列表:', components)
  } catch (error) {
    ElMessage.error('扫描失败: ' + error.message)
  }
}

const handleAdd = (row = null) => {
  isEdit.value = false
  currentMenu.value = row ? { parentId: row.id, parentName: row.name } : {}
  dialogVisible.value = true
}

const handleEdit = (row) => {
  isEdit.value = true
  currentMenu.value = { ...row }
  dialogVisible.value = true
}

const handleDelete = (row) => {
  ElMessage.info(`删除菜单: ${row.name}`)
}

const handleSubmit = (formData) => {
  if (isEdit.value) {
    ElMessage.success('编辑成功')
  } else {
    ElMessage.success('新增成功')
  }
  dialogVisible.value = false
  loadData()
}

loadData()
</script>

<style scoped>
.menu-management {
  height: 100%;
  padding: 8px;
}

.menu-management :deep(.el-card) {
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  height: 100%;
}

.menu-management :deep(.menu-card) {
  display: flex;
  flex-direction: column;
}

.menu-management :deep(.el-card__body) {
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

.table-wrapper {
  flex: 1;
  display: flex;
  flex-direction: column;
  overflow: hidden;
  min-height: 0;
}
</style>
