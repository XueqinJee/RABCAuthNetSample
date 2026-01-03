<template>
    <h2>测试</h2>
    <art-table :colums="columns" :forms="formFields" :data="tableData" :loading="isLoading" :page="pageination"
        @on-search-handle="loadData"
        @on-page-size-change-handle="onPageSizeChange" 
        @on-current-change-handle="onCurrentPageChange">
        <template #action="{ row, index }">
            <el-button size="small" @click="doSearch">编辑</el-button>
            <el-button size="small">修改密码</el-button>
            <el-button size="small">删除</el-button>
        </template>
    </art-table>
</template>

<script setup lang="ts">
import type { TableConfig, TableItemConfig } from '@/config/components/art.table.config'
import ArtTable from '@/components/art-tables/ArtTable.vue';
import { useTable } from '@/components/art-tables/useTable';
import {userApi} from '@/api'

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
            {prop: 'id',label: '编号', type: 'text', width: 80},
            {prop: 'nickName',label: '昵称', type: 'text', width: 200},
            {prop: 'userName',label: '登录名', type: 'text', width: 120},
            {prop: 'email',label: '邮箱', type: 'text', width: 200},
            {prop: 'createOn',label: '建立时间', type: 'date', width: 200},
            {prop: 'action', label: '操作', useSlot: true}
        ],
        formFactory: () => [
            {name: 'nickName', label: '', type: 'input'}
        ]
    }
})

const doSearch = () => {
    
}

</script>