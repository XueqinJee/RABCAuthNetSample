<template>
  <div class="login-container">
    <div class="login-left">
      <div class="logo-container">
        <div class="logo-icon">
          <svg viewBox="0 0 1024 1024" width="120" height="120">
            <path
              d="M512 64C264.6 64 64 264.6 64 512s200.6 448 448 448 448-200.6 448-448S759.4 64 512 64zm0 820c-205.4 0-372-166.6-372-372s166.6-372 372-372 372 166.6 372 372-166.6 372-372 372z"
              fill="#667eea"></path>
            <path
              d="M512 140c-205.4 0-372 166.6-372 372s166.6 372 372 372 372-166.6 372-372-166.6-372-372-372zm0 680c-170.1 0-308-137.9-308-308s137.9-308 308-308 308 137.9 308 308-137.9 308-308 308z"
              fill="#764ba2"></path>
            <path
              d="M512 280c-132.5 0-240 107.5-240 240s107.5 240 240 240 240-107.5 240-240-107.5-240-240-240zm0 416c-97 0-176-79-176-176s79-176 176-176 176 79 176 176-79 176-176 176z"
              fill="#667eea"></path>
          </svg>
        </div>
        <h1 class="logo-title">欢迎使用</h1>
        <p class="logo-subtitle">AcAuthNet 认证系统</p>
      </div>
    </div>
    <div class="login-right">
      <div class="login-card">
        <div class="card-header">
          <span>用户登录</span>
        </div>
        <el-form :model="loginForm" :rules="rules" ref="loginFormRef" label-width="80px" class="login-form">
          <el-form-item label="用户名" prop="username">
            <el-input v-model="loginForm.username" placeholder="请输入用户名" />
          </el-form-item>
          <el-form-item label="密码" prop="password">
            <el-input v-model="loginForm.password" type="password" placeholder="请输入密码" show-password />
          </el-form-item>
          <el-form-item class="small-margin">
            <el-checkbox v-model="loginForm.remember">记住密码</el-checkbox>
          </el-form-item>
          <el-form-item class="small-margin">
            <el-button :loading="loginLoading" type="primary" style="width: 100%" @click="handleLogin">
              <span v-if="!loginLoading">登录</span>
              <span v-else>正在登录...</span>
            </el-button>
          </el-form-item>
          <el-form-item class="small-margin">
            <div class="register-link">
              <span>还没有账号？</span>
              <el-link type="primary" @click="handleRegister">立即注册</el-link>
            </div>
          </el-form-item>
        </el-form>
        <div class="third-party-login">
          <el-divider>第三方登录</el-divider>
          <div class="social-icons">
            <el-button circle size="large" class="social-btn" @click="handleThirdPartyLogin('github')">
              <el-icon><svg viewBox="0 0 1024 1024" width="24" height="24">
                  <path
                    d="M512 42.666667A464.64 464.64 0 0 0 42.666667 502.186667 460.373333 460.373333 0 0 0 363.52 938.666667c23.466667 4.266667 32-9.813333 32-22.186667v-78.08c-130.56 27.733333-158.293333-61.44-158.293333-61.44a122.026667 122.026667 0 0 0-52.053334-67.413333c-42.666667-28.16 3.413333-27.733333 3.413334-27.733334a98.56 98.56 0 0 1 71.68 47.36 101.12 101.12 0 0 0 136.533333 37.973334 99.413333 99.413333 0 0 1 29.866667-61.44c-104.106667-11.52-213.333333-50.773333-213.333334-226.986667a177.066667 177.066667 0 0 1 47.36-124.16 161.28 161.28 0 0 1 4.693334-121.173333s39.68-12.373333 128 46.933333a455.68 455.68 0 0 1 234.666666 0c89.6-59.306667 128-46.933333 128-46.933333a161.28 161.28 0 0 1 4.693334 121.173333A177.066667 177.066667 0 0 1 810.666667 477.866667c0 176.64-110.08 215.466667-213.333334 226.986666a106.666667 106.666667 0 0 1 32 85.333334v125.866666c0 14.933333 8.533333 26.88 32 22.186667A460.8 460.8 0 0 0 981.333333 502.186667 464.64 464.64 0 0 0 512 42.666667"
                    fill="#333"></path>
                </svg></el-icon>
            </el-button>
            <el-button circle size="large" class="social-btn" @click="handleThirdPartyLogin('wechat')">
              <el-icon><svg viewBox="0 0 1024 1024" width="24" height="24">
                  <path
                    d="M664.9 545.5c-5.8 0-11.5 0.5-17.1 1.4 1.8-8.3 2.8-16.9 2.8-25.7 0-82.9-76.6-150-171.1-150-94.5 0-171.1 67.1-171.1 150 0 82.9 76.6 150 171.1 150 19.6 0 38.6-2.7 56.5-7.7 13.1 9.3 28.5 16.4 45.2 20.7 3.2 0.8 6.5 1.5 9.8 2.1-3.8-6.3-6.8-13.1-8.9-20.3 25.3 13.9 55.1 21.9 87 21.9 5.8 0 11.5-0.5 17.1-1.4-1.8 8.3-2.8 16.9-2.8 25.7 0 82.9 76.6 150 171.1 150 94.5 0 171.1-67.1 171.1-150 0-82.9-76.6-150-171.1-150-31.9 0-61.7 8-87 21.9 2.1 7.2 5.1 14 8.9 20.3-3.3-0.6-6.6-1.3-9.8-2.1-16.7-4.3-32.1-11.4-45.2-20.7-17.9 5-36.9 7.7-56.5 7.7-94.5 0-171.1-67.1-171.1-150 0-8.8 1-17.4 2.8-25.7 5.6-0.9 11.3-1.4 17.1-1.4 31.9 0 61.7-8 87-21.9-2.1-7.2-5.1-14-8.9-20.3 3.3 0.6 6.6 1.3 9.8 2.1 16.7 4.3 32.1 11.4 45.2 20.7 17.9-5 36.9-7.7 56.5-7.7 94.5 0 171.1 67.1 171.1 150 0 8.8-1 17.4-2.8 25.7-5.6 0.9-11.3 1.4-17.1 1.4z"
                    fill="#07C160"></path>
                </svg></el-icon>
            </el-button>
            <el-button circle size="large" class="social-btn" @click="handleThirdPartyLogin('qq')">
              <el-icon><svg viewBox="0 0 1024 1024" width="24" height="24">
                  <path
                    d="M824.8 613.2c-16-51.4-34.4-94.6-62.7-165.3C766.5 262.2 689.3 112 511.5 112 331.7 112 256.2 265.2 261 447.9c-28.4 70.8-46.7 113.7-62.7 165.3-34 109.5-23 154.8-14.6 155.8 18 2.2 70.1-82.4 70.1-82.4 0 49 25.2 112.9 79.8 159-26.4 8.1-85.7 29.9-71.6 53.8 11.4 19.3 196.2 12.3 249.5 6.3 53.3 6 238.1 13 249.5-6.3 14.1-23.8-45.2-45.7-71.6-53.8 54.6-46.2 79.8-110.1 79.8-159 0 0 52.1 84.6 70.1 82.4 8.5-1.1 19.5-46.4-14.5-155.8z"
                    fill="#12B7F5"></path>
                </svg></el-icon>
            </el-button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, reactive } from 'vue'
import { ElMessage } from 'element-plus'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores'

import { userApi } from '@/api'

const router = useRouter()
const loginFormRef = ref(null)
const loginLoading = ref(false)

const loginForm = reactive({
  username: 'admin',
  password: 'bw@123',
  remember: false
})

const rules = {
  username: [
    { required: true, message: '请输入用户名', trigger: 'blur' }
  ],
  password: [
    { required: true, message: '请输入密码', trigger: 'blur' },
    { min: 6, message: '密码长度不能少于6位', trigger: 'blur' }
  ]
}

const handleLogin = async () => {
  await loginFormRef.value.validate(async (valid, fields) => {
    if (!valid) return;
    try {
      loginLoading.value = true
      // 登录
      const userStore = useUserStore()
      var login = await userStore.loginUserAsync(loginForm.username, loginForm.password)
      if(login){
        router.push('main')
      }
    }
    finally {
      loginLoading.value = false
    }
  })
}

const handleRegister = () => {
  router.push('/register')
}

const handleThirdPartyLogin = (platform) => {
  ElMessage.info(`${platform} 登录功能待实现`)
}
</script>

<style scoped>
.login-container {
  display: flex;
  min-height: 100vh;
}

.login-left {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

.logo-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  color: white;
}

.logo-icon {
  margin-bottom: 30px;
  animation: float 3s ease-in-out infinite;
}

@keyframes float {

  0%,
  100% {
    transform: translateY(0);
  }

  50% {
    transform: translateY(-10px);
  }
}

.logo-title {
  font-size: 36px;
  font-weight: bold;
  margin: 0 0 15px 0;
}

.logo-subtitle {
  font-size: 18px;
  margin: 0;
  opacity: 0.9;
}

.login-right {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
  background: white;
  padding: 40px;
}

.login-card {
  width: 100%;
  max-width: 450px;
  padding: 40px;
}

.card-header {
  text-align: center;
  font-size: 28px;
  font-weight: bold;
  color: #333;
  margin-bottom: 30px;
}

.login-form :deep(.el-form-item) {
  margin-bottom: 20px;
}

.login-form :deep(.small-margin) {
  margin-bottom: 12px;
}

.register-link {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 8px;
  font-size: 14px;
}

.third-party-login {
  margin-top: 30px;
}

.social-icons {
  display: flex;
  justify-content: center;
  gap: 30px;
  margin-top: 20px;
}

.social-btn {
  width: 50px;
  height: 50px;
  transition: transform 0.2s;
}

.social-btn :deep(.el-icon) {
  font-size: 24px;
}

.social-btn:hover {
  transform: scale(1.1);
}
</style>
