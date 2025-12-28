<template>
  <div class="sidebar" :class="{ 'sidebar-collapse': isCollapse }">
    <div class="logo-section">
      <div class="logo-icon">
        <svg viewBox="0 0 1024 1024" width="32" height="32">
          <path d="M512 64C264.6 64 64 264.6 64 512s200.6 448 448 448 448-200.6 448-448S759.4 64 512 64zm0 820c-205.4 0-372-166.6-372-372s166.6-372 372-372 372 166.6 372 372-166.6 372-372 372z" fill="#667eea"></path>
          <path d="M512 140c-205.4 0-372 166.6-372 372s166.6 372 372 372 372-166.6 372-372-166.6-372-372-372zm0 680c-170.1 0-308-137.9-308-308s137.9-308 308-308 308 137.9 308 308-137.9 308-308 308z" fill="#764ba2"></path>
          <path d="M512 280c-132.5 0-240 107.5-240 240s107.5 240 240 240 240-107.5 240-240-107.5-240-240-240zm0 416c-97 0-176-79-176-176s79-176 176-176 176 79 176 176-79 176-176 176z" fill="#667eea"></path>
        </svg>
      </div>
      <span class="logo-text">AcAuthNet</span>
    </div>
    <el-menu
      :default-active="activeMenu"
      class="sidebar-menu"
      :collapse="isCollapse"
      :collapse-transition="false"
      @select="handleSelect"
    >
      <el-menu-item index="dashboard">
        <el-icon><HomeFilled /></el-icon>
        <template #title>仪表盘</template>
      </el-menu-item>
      <el-sub-menu index="settings">
        <template #title>
          <el-icon><Setting /></el-icon>
          <span>设置</span>
        </template>
        <el-menu-item index="user">
          <el-icon><User /></el-icon>
          <template #title>用户管理</template>
        </el-menu-item>
        <el-menu-item index="role">
          <el-icon><Lock /></el-icon>
          <template #title>角色管理</template>
        </el-menu-item>
        <el-menu-item index="permission">
          <el-icon><Key /></el-icon>
          <template #title>权限管理</template>
        </el-menu-item>
        <el-menu-item index="settings">
          <el-icon><Setting /></el-icon>
          <template #title>系统设置</template>
        </el-menu-item>
      </el-sub-menu>
    </el-menu>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { HomeFilled, User, Lock, Key, Setting } from '@element-plus/icons-vue'

const props = defineProps({
  isCollapse: {
    type: Boolean,
    default: false
  }
})

const emit = defineEmits(['update:isCollapse'])

const router = useRouter()
const route = useRoute()

const activeMenu = computed(() => {
  return route.meta.menu || route.name || 'dashboard'
})

const handleSelect = (index) => {
  router.push({ name: index })
}
</script>

<style scoped>
.sidebar {
  width: 240px;
  background: linear-gradient(180deg, #667eea 0%, #764ba2 100%);
  display: flex;
  flex-direction: column;
  transition: width 0.3s;
  flex-shrink: 0;
}

.sidebar-collapse {
  width: 64px;
}

.logo-section {
  display: flex;
  align-items: center;
  height: 60px;
  padding: 0 16px;
  gap: 12px;
  flex-shrink: 0;
  transition: all 0.3s;
}

.sidebar-collapse .logo-section {
  justify-content: center;
  padding: 0;
}

.sidebar-collapse .logo-text {
  display: none;
}

.logo-icon {
  flex-shrink: 0;
}

.logo-icon svg {
  width: 32px;
  height: 32px;
}

.logo-text {
  color: white;
  font-size: 16px;
  font-weight: bold;
  white-space: nowrap;
}

.sidebar-menu {
  flex: 1;
  border: none;
  background: transparent;
  padding-top: 8px;
}

.sidebar-menu :deep(.el-sub-menu__title) {
  color: rgba(255, 255, 255, 0.8);
  height: 44px;
  line-height: 44px;
  margin: 2px 8px;
  border-radius: 6px;
}

.sidebar-menu :deep(.el-sub-menu__title:hover) {
  background: rgba(255, 255, 255, 0.1);
  color: white;
}

.sidebar-menu :deep(.el-sub-menu .el-menu) {
  background: transparent;
}

.sidebar-menu :deep(.el-menu-item) {
  color: rgba(255, 255, 255, 0.8);
  height: 44px;
  line-height: 44px;
  margin: 2px 8px;
  border-radius: 6px;
}

.sidebar-menu :deep(.el-menu-item:hover) {
  background: rgba(255, 255, 255, 0.1);
  color: white;
}

.sidebar-menu :deep(.el-menu-item.is-active) {
  background: rgba(255, 255, 255, 0.2);
  color: white;
}

.sidebar-menu:deep(.el-menu--collapse) {
  width: 64px;
}

.sidebar-menu:deep(.el-menu--collapse .el-menu-item) {
  padding: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 2px 4px;
  height: 44px;
}

.sidebar-menu:deep(.el-menu--collapse .el-menu-item .el-icon) {
  margin: 0;
  font-size: 20px;
}

.sidebar-menu:deep(.el-menu--collapse .el-menu-item span) {
  display: none;
}

.sidebar-menu:deep(.el-menu--collapse .el-sub-menu__title) {
  padding: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 2px 4px;
  height: 44px;
}

.sidebar-menu:deep(.el-menu--collapse .el-sub-menu__title .el-icon) {
  margin: 0;
  font-size: 20px;
}

.sidebar-menu:deep(.el-menu--collapse .el-sub-menu__title span) {
  display: none;
}
</style>
