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
                    filterable
                    remote
                    v-model="formData[item.name]" 
                    :placeholder="'请选择' + item.label" 
                    :style="{'width': (item.width || 120) + 'px'}"
                    :remote-method="(keyword) => handleRemoteSearch(item, keyword)"
                    :loading="item.loading"
                    @change="(value) => handleSelectChange(item, value)">
                    <el-option v-for="row in item.source || []" :key="row[getFieldKey(item, 'value')]" :label="row[getFieldKey(item, 'label')]" :value="row[getFieldKey(item, 'value')]"/>
                </el-select>
            </el-form-item>
            <el-form-item v-else-if="item.type == 'checkbox'" :label="item.label">
                <el-checkbox-group v-model="formData[item.name]">
                    <el-checkbox v-for="option in item.source || []" :key="option.value" :label="option.value">{{ option.label }}</el-checkbox>
                </el-checkbox-group>
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
import { ref, onMounted } from 'vue'

const props = defineProps({
    config: Array<FormFieldOption>
})
const emits = defineEmits(['onSearch'])
const remoteSearchRef = ref({})
const formData = ref({})

// 获取字段映射键名
const getFieldKey = (item: FormFieldOption, type: 'label' | 'value') => {
    if (item.isRemoteData && item.remoteFieldMapping) {
        return item.remoteFieldMapping[type]
    }
    debugger
    return item.options?.[type] || type
}

// 初始化表单数据
props.config.forEach(item => {
    formData.value[item.name] = item.defaultValue ?? ''
    // 如果是远程数据且有预设值，则初始化
    if (item.isRemoteData && item.remoteCallFunc) {
        remoteSearchRef[item.name] = {
            souce: [],
            loading: false,
            isRemoteData: item.isRemoteData,
            remoteCallFunc: item.remoteCallFunc
        }
    }
})

// 远程搜索处理
const handleRemoteSearch = async (item: FormFieldOption, keyword: string) => {
    console.log(item);
    
    if (!item || !item.isRemoteData || !item.remoteCallFunc) return
    
    item.loading = true
    try {
        const response = await item.remoteCallFunc(keyword)
        // 假设API返回格式为 { data: [{label: '显示文本', value: '值', ...}, ...] }
        item.source = response.data || response || []
    } catch (error) {
        console.error('远程搜索失败:', error)
        item.source = []
    } finally {
        item.loading = false
    }
}

// 选择项变化处理
const handleSelectChange = (item: FormFieldOption, value: any) => {
    // 可以在这里添加额外的处理逻辑
    console.log(`选择项变化 - ${item.label}:`, value)
}

// 初始化时加载远程数据（如果有默认值）
onMounted(() => {
    props.config.forEach(async (item) => {
        if (item.isRemoteData && item.remoteCallFunc) {
            await handleRemoteSearch(remoteSearchRef.value[item.name], '')
        }
    })
})

const resetSearchFormHandle = () => {
    var keys = Object.keys(formData.value)
    keys.forEach(d => {
        const item = props.config.find(config => config.name === d)
        formData.value[d] = item?.defaultValue ?? ''
    })
    onSearchHandle()
}

const onSearchHandle = () => {
    emits('onSearch', {...formData.value})
}

</script>