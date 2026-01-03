<template>
  <el-dialog
    v-model="dialogVisible"
    :title="dialogTitle"
    width="500px"
    @close="handleDialogClose"
  >
    <el-form
      ref="formRef"
      :model="form"
      :rules="rules"
      label-width="80px"
    >
      <el-form-item label="角色名称" prop="roleName">
        <el-input v-model="form.roleName" placeholder="请输入角色名称" />
      </el-form-item>
      <el-form-item label="角色编码" prop="roleCode">
        <el-input v-model="form.roleCode" placeholder="请输入角色编码" />
      </el-form-item>
      <el-form-item label="角色描述" prop="description">
        <el-input v-model="form.description" type="textarea" placeholder="请输入角色描述" rows="3" />
      </el-form-item>
      <el-form-item label="排序" prop="sort">
        <el-input-number v-model="form.sort" :min="0" placeholder="排序数值" />
      </el-form-item>
      <el-form-item label="状态" prop="isEnabled">
        <el-radio-group v-model="form.isEnabled">
          <el-radio :label="true">启用</el-radio>
          <el-radio :label="false">禁用</el-radio>
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
  name: 'RoleDialog'
}
</script>

<script setup>
import { ref, computed, watch } from 'vue'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  isEdit: {
    type: Boolean,
    default: false
  },
  roleData: {
    type: Object,
    default: () => ({})
  }
})

const emit = defineEmits(['update:visible', 'submit'])

const dialogVisible = ref(false)
const formRef = ref(null)
const form = ref({
  id: null,
  roleName: '',
  roleCode: '',
  description: '',
  isEnabled: true,
  sort: 0
})

const dialogTitle = computed(() => {
  return props.isEdit ? '编辑角色' : '新增角色'
})

const rules = {
  roleName: [
    { required: true, message: '请输入角色名称', trigger: 'blur' },
    { min: 2, max: 20, message: '角色名称长度在 2 到 20 个字符', trigger: 'blur' }
  ],
  roleCode: [
    { required: true, message: '请输入角色编码', trigger: 'blur' },
    { pattern: /^[A-Z_][A-Z0-9_]*$/, message: '角色编码只能包含大写字母、数字和下划线，且不能以数字开头', trigger: 'blur' }
  ],
  description: [
    { required: true, message: '请输入角色描述', trigger: 'blur' },
    { max: 100, message: '角色描述不能超过100个字符', trigger: 'blur' }
  ],
  sort: [
    { required: true, message: '请输入排序数值', trigger: 'blur' },
    { type: 'number', min: 0, message: '排序数值必须大于等于0', trigger: 'blur' }
  ]
}

watch(() => props.visible, (newVal) => {
  dialogVisible.value = newVal
})

watch(dialogVisible, (newVal) => {
  emit('update:visible', newVal)
})

watch(() => props.roleData, (newVal) => {
  if (newVal && Object.keys(newVal).length > 0) {
    form.value = {
      id: newVal.id,
      roleName: newVal.roleName,
      roleCode: newVal.roleCode,
      description: newVal.description,
      isEnabled: newVal.isEnabled,
      sort: newVal.sort || 0
    }
  }
}, { deep: true, immediate: true })

const handleDialogClose = () => {
  formRef.value?.resetFields()
  form.value = {
    id: null,
    roleName: '',
    roleCode: '',
    description: '',
    isEnabled: true,
    sort: 0
  }
}

const handleSubmit = () => {
  formRef.value?.validate((valid) => {
    if (valid) {
      emit('submit', props.isEdit, { ...form.value })
    }
  })
}
</script>