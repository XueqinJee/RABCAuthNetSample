<template>
  <el-dialog
    v-model="dialogVisible"
    :title="dialogTitle"
    width="600px"
    @close="handleDialogClose"
  >
    <el-form
      ref="formRef"
      :model="form"
      :rules="rules"
      label-width="100px"
    >
      <el-form-item label="上级菜单" prop="parentId">
        <el-tree-select
          v-model="form.parentId"
          :data="parentMenuOptions"
          :props="{ label: 'name', value: 'id' }"
          placeholder="请选择上级菜单"
          check-strictly
          clearable
          style="width: 100%"
        />
      </el-form-item>
      <el-form-item label="菜单类型" prop="type">
        <el-radio-group v-model="form.type">
          <el-radio label="目录">目录</el-radio>
          <el-radio label="菜单">菜单</el-radio>
          <el-radio label="按钮">按钮</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="菜单名称" prop="name">
        <el-input v-model="form.name" placeholder="请输入菜单名称" />
      </el-form-item>
      <el-form-item label="路由路径" prop="path" v-if="form.type !== '按钮'">
        <el-input v-model="form.path" placeholder="请输入路由路径" />
      </el-form-item>
      <el-form-item label="组件路径" prop="component" v-if="form.type === '菜单'">
        <el-select
          v-model="form.component"
          filterable
          placeholder="请选择或输入组件路径"
          style="width: 100%"
        >
          <el-option
            v-for="item in componentOptions"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="权限标识" prop="permission" v-if="form.type === '按钮'">
        <el-input v-model="form.permission" placeholder="请输入权限标识，如: system:user:add" />
      </el-form-item>
      <el-form-item label="菜单图标" prop="icon" v-if="form.type !== '按钮'">
        <IconSelector v-model="form.icon" />
      </el-form-item>
      <el-form-item label="显示排序" prop="sort">
        <el-input-number v-model="form.sort" :min="0" :max="999" controls-position="right" style="width: 100%" />
      </el-form-item>
      <el-form-item label="菜单状态" prop="status">
        <el-radio-group v-model="form.status">
          <el-radio label="启用">启用</el-radio>
          <el-radio label="禁用">禁用</el-radio>
        </el-radio-group>
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="dialogVisible = false">取消</el-button>
      <el-button type="primary" @click="handleSubmit">确定</el-button>
    </template>
  </el-dialog>
</template>

<script>
export default {
  name: 'MenuDialog'
}
</script>

<script setup>
import { ref, computed, watch } from 'vue'
import { getComponentOptions } from '@/utils/componentScanner'
import IconSelector from '@/components/IconSelector.vue'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  isEdit: {
    type: Boolean,
    default: false
  },
  menuData: {
    type: Object,
    default: () => ({})
  },
  menuOptions: {
    type: Array,
    default: () => []
  }
})

const emit = defineEmits(['update:visible', 'submit'])

const dialogVisible = ref(false)
const formRef = ref(null)
const form = ref({
  id: null,
  parentId: null,
  type: '菜单',
  name: '',
  path: '',
  component: '',
  permission: '',
  icon: '',
  sort: 0,
  status: '启用'
})

const dialogTitle = computed(() => {
  return props.isEdit ? '编辑菜单' : '新增菜单'
})

const parentMenuOptions = computed(() => {
  return props.menuOptions.map(item => ({
    ...item,
    children: []
  }))
})

const componentOptions = computed(() => {
  return getComponentOptions()
})

const rules = {
  name: [
    { required: true, message: '请输入菜单名称', trigger: 'blur' }
  ],
  type: [
    { required: true, message: '请选择菜单类型', trigger: 'change' }
  ],
  path: [
    { required: true, message: '请输入路由路径', trigger: 'blur' }
  ],
  component: [
    { required: true, message: '请输入组件路径', trigger: 'blur' }
  ],
  permission: [
    { required: true, message: '请输入权限标识', trigger: 'blur' }
  ]
}

watch(() => props.visible, (newVal) => {
  dialogVisible.value = newVal
})

watch(dialogVisible, (newVal) => {
  emit('update:visible', newVal)
})

watch(() => props.menuData, (newVal) => {
  if (newVal && Object.keys(newVal).length > 0) {
    if (props.isEdit) {
      form.value = {
        id: newVal.id,
        parentId: newVal.parentId,
        type: newVal.type,
        name: newVal.name,
        path: newVal.path,
        component: newVal.component,
        permission: newVal.permission || '',
        icon: newVal.icon,
        sort: newVal.sort,
        status: newVal.status
      }
    } else {
      form.value = {
        id: null,
        parentId: newVal.parentId || null,
        type: '菜单',
        name: '',
        path: '',
        component: '',
        permission: '',
        icon: '',
        sort: 0,
        status: '启用'
      }
    }
  }
}, { deep: true, immediate: true })

const handleDialogClose = () => {
  formRef.value?.resetFields()
  form.value = {
    id: null,
    parentId: null,
    type: '菜单',
    name: '',
    path: '',
    component: '',
    permission: '',
    icon: '',
    sort: 0,
    status: '启用'
  }
}

const handleSubmit = () => {
  formRef.value?.validate((valid) => {
    if (valid) {
      emit('submit', { ...form.value })
    }
  })
}
</script>
