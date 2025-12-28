<template>
  <div class="header">
    <div class="header-left">
      <el-button text @click="toggleCollapse">
        <el-icon :size="20"><Fold v-if="!isCollapse" /><Expand v-else /></el-icon>
      </el-button>
      <el-breadcrumb separator="/">
        <el-breadcrumb-item>首页</el-breadcrumb-item>
        <el-breadcrumb-item>{{ currentMenu }}</el-breadcrumb-item>
      </el-breadcrumb>
    </div>
    <div class="header-right">
      <el-button text>
        <el-icon :size="20"><Bell /></el-icon>
      </el-button>
      <el-dropdown @command="handleCommand">
        <div class="user-info">
          <el-avatar :size="32" src="https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png"></el-avatar>
          <span class="username">管理员</span>
        </div>
        <template #dropdown>
          <el-dropdown-menu>
            <el-dropdown-item command="profile">个人中心</el-dropdown-item>
            <el-dropdown-item command="password">修改密码</el-dropdown-item>
            <el-dropdown-item divided command="logout">退出登录</el-dropdown-item>
          </el-dropdown-menu>
        </template>
      </el-dropdown>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import { Fold, Expand, Bell } from '@element-plus/icons-vue'

const props = defineProps({
  isCollapse: {
    type: Boolean,
    default: false
  }
})

const emit = defineEmits(['update:isCollapse'])

const router = useRouter()
const route = useRoute()

const currentMenu = computed(() => {
  const menuMap = {
    dashboard: '仪表盘',
    user: '用户管理',
    role: '角色管理',
    permission: '权限管理',
    settings: '系统设置'
  }
  return menuMap[route.meta.menu || route.name] || '仪表盘'
})

const toggleCollapse = () => {
  emit('update:isCollapse', !props.isCollapse)
}

const handleCommand = (command) => {
  switch (command) {
    case 'profile':
      ElMessage.info('个人中心')
      break
    case 'password':
      ElMessage.info('修改密码')
      break
    case 'logout':
      ElMessage.success('退出登录成功')
      router.push('/login')
      break
  }
}
</script>

<style scoped>
.header {
  height: 60px;
  background: white;
  border-bottom: 1px solid #e8e8e8;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 20px;
  flex-shrink: 0;
}

.header-left {
  display: flex;
  align-items: center;
  gap: 16px;
}

.header-right {
  display: flex;
  align-items: center;
  gap: 20px;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 8px;
  cursor: pointer;
}

.username {
  font-size: 14px;
  color: #333;
}
</style>
