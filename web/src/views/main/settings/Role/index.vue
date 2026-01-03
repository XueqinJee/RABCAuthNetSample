<template>
    <div class="role-management">
        <el-card class="role-card">
            <template #header>
                <div class="card-header">
                    <span>角色管理</span>
                    <el-button type="primary" @click="handleAdd">新增角色</el-button>
                </div>
            </template>

            <art-table :colums="columns" :forms="formFields" :data="tableData" :loading="isLoading" :page="pageination"
                @on-search-handle="loadData" @on-page-size-change-handle="onPageSizeChange"
                @on-current-change-handle="onCurrentPageChange">
                <template #action="{ row, index }">
                    <el-button size="small" link @click="handleEdit(row)">编辑</el-button>
                    <el-button size="small" link type="danger" @click="handleDelete(row)">删除</el-button>
                </template>
            </art-table>
        </el-card>

        <RoleDialog v-model:visible="dialogVisible" :is-edit="isEdit" :role-data="currentRole" @submit="onSubmit" />
    </div>
</template>

<script>
export default {
    name: 'RoleView'
}
</script>

<script setup>
import { ref, computed, watch } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import RoleDialog from './RoleDialog.vue'

import ArtTable from '@/components/art-tables/ArtTable.vue'
import { useTable } from '@/components/art-tables/useTable'
import { roleApi } from '@/api'

const dialogVisible = ref(false)
const isEdit = ref(false)
const currentRole = ref({})

const handleAdd = () => {
    isEdit.value = false
    currentRole.value = {}
    dialogVisible.value = true
}

const handleEdit = (row) => {
    isEdit.value = true
    currentRole.value = { ...row }
    dialogVisible.value = true
}

const handleDelete = (row) => {
    ElMessageBox.confirm(
        `确认删除角色 "${row.roleName}" 吗？`,
        '删除确认',
        {
            confirmButtonText: '确认删除',
            cancelButtonText: '取消',
            type: 'warning',
        }
    ).then(async () => {
        try {
            await roleApi.deleteRole(row.id)
            ElMessage.success('角色删除成功')
            await loadData()
        } catch (error) {
            ElMessage.error('角色删除失败')
        }
    }).catch(() => {
        ElMessage.info('已取消删除')
    })
}

const onSubmit = async (isEdit, data) => {
    try {
        if (isEdit) {
            await roleApi.editRole(data)
            ElMessage.success('角色更新成功')
        } else {
            await roleApi.addRole(data)
            ElMessage.success('角色创建成功')
        }
        await loadData()
        dialogVisible.value = false
    } catch (error) {
        if (isEdit) {
            ElMessage.error('角色更新失败')
        } else {
            ElMessage.error('角色创建失败')
        }
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
        apiFn: roleApi.getRoleData,
        columnsFactory: () => [
            { prop: 'id', label: '编号', type: 'text', width: 80 },
            { prop: 'roleName', label: '角色名称', type: 'text', width: 150 },
            { prop: 'roleCode', label: '角色编码', type: 'text', width: 120 },
            { prop: 'description', label: '角色描述', type: 'text', width: 300 },
            { prop: 'sort', label: '排序', type: 'tag', width: 80 },
            { prop: 'isEnabled', label: '状态', type: 'switch', width: 100, disabled: true},
            { prop: 'createOn', label: '创建时间', type: 'date', width: 180 },
            { prop: 'action', label: '操作', useSlot: true }
        ],
        formFactory: () => [
            { name: 'roleName', label: '角色名称', type: 'input' },
            { name: 'roleCode', label: '角色编码', type: 'input' },
        ]
    }
})

</script>

<style scoped>
.role-management {
    height: 100%;
    padding: 8px;
}

.role-management :deep(.el-card) {
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
    height: 100%;
}

.role-management :deep(.role-card) {
    display: flex;
    flex-direction: column;
}

.role-management :deep(.el-card__body) {
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