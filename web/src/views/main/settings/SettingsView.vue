<template>
  <div class="settings">
    <el-card>
      <template #header>
        <span>系统设置</span>
      </template>
      <el-form :model="form" label-width="120px">
        <el-form-item label="系统名称">
          <el-input v-model="form.systemName" placeholder="请输入系统名称" />
        </el-form-item>
        <el-form-item label="系统描述">
          <el-input v-model="form.systemDescription" type="textarea" :rows="3" placeholder="请输入系统描述" />
        </el-form-item>
        <el-form-item label="管理员邮箱">
          <el-input v-model="form.adminEmail" placeholder="请输入管理员邮箱" />
        </el-form-item>
        <el-form-item label="启用注册">
          <el-switch v-model="form.enableRegister" />
        </el-form-item>
        <el-form-item label="会话超时">
          <el-input-number v-model="form.sessionTimeout" :min="1" :max="1440" />
          <span style="margin-left: 8px">分钟</span>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSave">保存设置</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
export default {
  name: 'SettingsView'
}
</script>

<script setup>
import { reactive } from 'vue'
import { ElMessage } from 'element-plus'

const form = reactive({
  systemName: 'AcAuthNet',
  systemDescription: '功能完善的权限管理系统',
  adminEmail: 'admin@example.com',
  enableRegister: true,
  sessionTimeout: 30
})

const handleSave = () => {
  ElMessage.success('保存成功')
}

const handleReset = () => {
  ElMessage.info('重置成功')
}
</script>

<style scoped>
.settings {
  height: 100%;
}
</style>
