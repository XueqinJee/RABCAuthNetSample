<template>
  <div class="layout-container">
    <Sidebar :is-collapse="isCollapse" />
    <div class="main-content">
      <Header :is-collapse="isCollapse" @update:is-collapse="handleCollapse" />
      <Tabs 
        :tabs="tabs" 
        :active-tab="activeTab" 
        @tab-click="handleTabClick" 
        @tab-remove="handleTabRemove" 
      />
      <div class="content-area">
        <keep-alive :include="cachedViews">
          <router-view />
        </keep-alive>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { HomeFilled, User, Lock, Key, Setting, Menu } from '@element-plus/icons-vue'
import Sidebar from './Sidebar.vue'
import Header from './Header.vue'
import Tabs from './Tabs.vue'

const router = useRouter()
const route = useRoute()

const isCollapse = ref(false)

const menuMap = {
  dashboard: { title: '仪表盘', icon: HomeFilled, closable: false, showTitle: false, componentName: 'DashboardView' },
  user: { title: '用户管理', icon: User, closable: true, showTitle: true, componentName: 'UserView' },
  role: { title: '角色管理', icon: Lock, closable: true, showTitle: true, componentName: 'RoleView' },
  permission: { title: '权限管理', icon: Key, closable: true, showTitle: true, componentName: 'PermissionView' },
  settings: { title: '系统设置', icon: Setting, closable: true, showTitle: true, componentName: 'SettingsView' },
  menu: { title: '菜单管理', icon: Menu, closable: true, showTitle: true, componentName: 'MenuView' }
}

const tabs = ref([
  { name: 'dashboard', label: '仪表盘', title: '仪表盘', icon: HomeFilled, closable: false, showTitle: false }
])

const activeTab = ref('dashboard')

const cachedViews = computed(() => {
  return tabs.value.map(tab => {
    const menuInfo = menuMap[tab.name]
    return menuInfo ? menuInfo.componentName : ''
  }).filter(name => name)
})

watch(() => route.name, (newName) => {
  if (newName && menuMap[newName]) {
    const existingTab = tabs.value.find(tab => tab.name === newName)
    if (!existingTab) {
      const menuInfo = menuMap[newName]
      tabs.value.push({
        name: newName,
        label: menuInfo.title,
        title: menuInfo.title,
        icon: menuInfo.icon,
        closable: menuInfo.closable,
        showTitle: menuInfo.showTitle
      })
    }
    activeTab.value = newName
  }
}, { immediate: true })

const handleCollapse = (value) => {
  isCollapse.value = value
}

const handleTabClick = (tabName) => {
  router.push({ name: tabName })
}

const handleTabRemove = (tabName) => {
  const tabIndex = tabs.value.findIndex(tab => tab.name === tabName)
  if (tabIndex > -1) {
    tabs.value.splice(tabIndex, 1)
    if (activeTab.value === tabName) {
      const nextTab = tabs.value[Math.max(0, tabIndex - 1)]
      if (nextTab) {
        activeTab.value = nextTab.name
        router.push({ name: nextTab.name })
      }
    }
  }
}
</script>

<style scoped>
.layout-container {
  display: flex;
  height: 100vh;
  overflow: hidden;
}

.main-content {
  flex: 1;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}

.content-area {
  flex: 1;
  height: 100%;
  padding: 5px;
  overflow-y: hidden;
  background: #f5f5f5;
}
</style>
