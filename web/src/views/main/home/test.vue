<template>
    <h2>测试</h2>
    <art-table :config="useTableConfig()"></art-table>
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
                type: 'input'
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
        header: null,
        rowKey: 'id'
    }
}

</script>