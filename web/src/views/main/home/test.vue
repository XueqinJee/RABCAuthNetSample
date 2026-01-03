<template>
    <h2>测试</h2>
    <art-table :config="useTableConfig()">
        <template #action="{ row, index }">
            <section> {{ row.id }}</section>
        </template>
    </art-table>
</template>

<script setup lang="ts">
import type { TableConfig, TableItemConfig } from '@/config/components/art.table.config'
import ArtTable from '@/components/art-tables/ArtTable.vue';

const useTableConfig = (): TableConfig => {
    return {
        form: [
            {
                name: 'name',
                label: '标题',
                type: 'input'
            },
            {
                name: 'nickName',
                label: '昵称',
                type: 'input',
            },
            {
                name: 'email',
                label: '邮箱',
                type: 'input'
            },
            {
                name: 'state',
                label: '状态',
                type: 'select',
                width: 130,
                source: [
                    { id: 1, label: '启动', value: 1 },
                    { id: 2, label: '准备', value: 2 },
                    { id: 3, label: '禁用', value: 3 },
                ],
                options: {
                    label: 'label',
                    value: 'value'
                }
            }
        ],
        header: [
            {
                name: 'id',
                label: '编号',
                width: 120,
                type: 'text'
            },
            {
                name: 'name',
                label: '名称',
                width: 100,
                type: 'text'
            },
            {
                name: 'description',
                label: '描述',
                type: 'text'
            },
            {
                name: 'status',
                label: '状态',
                type: 'tag',
                width: 100,
                status: 'danger'
            },
            {
                name: 'switch',
                label: 'switch',
                type: 'switch',
                width: 120,
                disabled: true
            },
            {
                name: 'action',
                label: '操作',
                type: 'solt',
                width: 150
            }
        ],
        rowKey: 'id',
        data: [
            { id: 1, name: '小米awdfserfgregergergrefeawsdwad', status: '准备',description: '你好' }
        ]
    }
}

</script>