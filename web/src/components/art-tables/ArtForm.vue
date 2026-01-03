<template>
    <el-form :inline="true">
        <template v-for="item in props.config">
            <el-form-item v-if="item.type == 'input'" :label="item.label">
                <el-input 
                    :style="{'width': (item.width || 200) + 'px'}"
                    v-model="formData[item.name]" 
                    :placeholder="'请输入' + item.label" 
                    clearable/>
            </el-form-item>
            <el-form-item v-else-if="item.type == 'select'" :label="item.label">
                <el-select 
                    clearable
                    v-model="formData[item.name]" 
                    :placeholder="'请选择' + item.label" 
                    :style="{'width': (item.width || 120) + 'px'}">
                    <el-option v-for="row in item.source" :key="row[item.options.value]" :label="row[item.options.label]" :value="row[item.options.value]"/>
                </el-select>
            </el-form-item>
        </template>
        <el-form-item>
            <el-button @click="onSearchHandle" type="primary">搜索</el-button>
            <el-button @click="resetSearchFormHandle">重置</el-button>
        </el-form-item>
    </el-form>
</template>

<script setup lang="ts">
import { FormFieldOption } from './useTable';
import { ref } from 'vue'

const props = defineProps({
    config: Array<FormFieldOption>
})
const emits = defineEmits(['onSearch'])

const formData = ref({})

props.config.forEach(item => {
    formData.value[item.name] = item.defaultValue ?? ''
})

const resetSearchFormHandle = () => {
    var keys = Object.keys(formData.value)
    keys.forEach(d => {
        formData.value[d] = ''
    })
    onSearchHandle()
}

const onSearchHandle = () => {
    emits('onSearch', {...formData.value})
}

</script>