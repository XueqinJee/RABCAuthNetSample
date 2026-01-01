<template>
    <el-form :inline="true">
        <template v-for="item in props.config">
            <el-form-item v-if="item.type == 'input'" :label="item.label">
                <el-input v-model="formData[item.name]" :placeholder="'请输入' + item.label"/>
            </el-form-item>
            <el-form-item v-else-if="item.type == 'select'" :label="item.label">
                <el-select v-model="formData[item.name]" :placeholder="'请输入' + item.label" style="width: 240px">
                    <el-option v-for="row in item.source" :key="row[item.options.value]" :label="row[item.options.label]" :value="row[item.options.value]"/>
                </el-select>
            </el-form-item>
        </template>
        <el-form-item>
            <el-button type="primary">搜索</el-button>
            <el-button>重置</el-button>
        </el-form-item>
    </el-form>
</template>

<script setup lang="ts">
import type { FormItemConfig } from '@/config/components/art.table.config'
import { ref } from 'vue'

const props = defineProps({
    config: Array<FormItemConfig>
})

const formData = ref({})
debugger
props.config.forEach(item => {
    formData.value[item.name] = item.defaultValue ?? ''
})
</script>