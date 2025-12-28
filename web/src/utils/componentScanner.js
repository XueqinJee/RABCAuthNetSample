import { ElMessage } from 'element-plus'

const componentList = [
  {
    path: 'main/DashboardView',
    name: 'DashboardView',
    title: '仪表盘',
    filePath: 'main/DashboardView.vue'
  },
  {
    path: 'main/settings/User/index',
    name: 'UserView',
    title: '用户管理',
    filePath: 'main/settings/User/index.vue'
  },
  {
    path: 'main/settings/RoleView',
    name: 'RoleView',
    title: '角色管理',
    filePath: 'main/settings/RoleView.vue'
  },
  {
    path: 'main/settings/PermissionView',
    name: 'PermissionView',
    title: '权限管理',
    filePath: 'main/settings/PermissionView.vue'
  },
  {
    path: 'main/settings/SettingsView',
    name: 'SettingsView',
    title: '系统设置',
    filePath: 'main/settings/SettingsView.vue'
  },
  {
    path: 'main/settings/Menu/index',
    name: 'MenuView',
    title: '菜单管理',
    filePath: 'main/settings/Menu/index.vue'
  }
]

export function scanComponents() {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve([...componentList])
    }, 500)
  })
}

export function getComponentOptions() {
  return componentList.map(item => ({
    label: `${item.title} (${item.path})`,
    value: item.path,
    name: item.name,
    title: item.title
  }))
}

export function searchComponents(keyword) {
  if (!keyword) {
    return getComponentOptions()
  }
  const lowerKeyword = keyword.toLowerCase()
  return componentList
    .filter(item => 
      item.title.toLowerCase().includes(lowerKeyword) ||
      item.path.toLowerCase().includes(lowerKeyword) ||
      item.name.toLowerCase().includes(lowerKeyword)
    )
    .map(item => ({
      label: `${item.title} (${item.path})`,
      value: item.path,
      name: item.name,
      title: item.title
    }))
}
