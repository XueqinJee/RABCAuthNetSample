import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import Layout from '../components/Layout.vue'
import DashboardView from '../views/main/DashboardView.vue'
import UserView from '../views/main/settings/User/index.vue'
import RoleView from '../views/main/settings/RoleView.vue'
import PermissionView from '../views/main/settings/PermissionView.vue'
import SettingsView from '../views/main/settings/SettingsView.vue'
import MenuView from '../views/main/settings/Menu/index.vue'

import { useUserStore } from '@/stores'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/main/dashboard'
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView
    },
    {
      path: '/main',
      component: Layout,
      redirect: '/main/dashboard',
      children: [
        {
          path: 'dashboard',
          name: 'dashboard',
          component: DashboardView,
          meta: { menu: 'dashboard', requiresAuth: true }
        },
        {
          path: 'user',
          name: 'user',
          component: UserView,
          meta: { menu: 'user' }
        },
        {
          path: 'role',
          name: 'role',
          component: () => import('@/views/main/home/test.vue'),
          meta: { menu: 'role' }
        },
        {
          path: 'permission',
          name: 'permission',
          component: PermissionView,
          meta: { menu: 'permission' }
        },
        {
          path: 'settings',
          name: 'settings',
          component: SettingsView,
          meta: { menu: 'settings' }
        },
        {
          path: 'menu',
          name: 'menu',
          component: MenuView,
          meta: { menu: 'menu' }
        }
      ]
    }
  ],
})

router.beforeEach((to, from, next) => {
  const userStore = useUserStore()

  if (to.meta.requiresAuth !== true) {
    next()
    return;
  }
  console.log(userStore.isLogin);
  
  if (userStore.isLogin) {
    next()
  } else {
    next('/login')
  }
})

export default router
