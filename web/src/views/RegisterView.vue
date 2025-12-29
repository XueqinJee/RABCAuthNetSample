<template>
  <div class="register-container">
    <div class="register-left">
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
        <h1 class="logo-title">加入我们</h1>
        <p class="logo-subtitle">AcAuthNet 认证系统</p>
      </div>
    </div>
    <div class="register-right">
      <div class="register-card">
        <div class="card-header">注册账号</div>
        <el-form :model="registerForm" :rules="rules" ref="registerFormRef" label-width="0" class="register-form">
          <el-form-item prop="username">
            <el-input v-model="registerForm.username" placeholder="用户名" :prefix-icon="User" size="large"></el-input>
          </el-form-item>
          <el-form-item prop="nickname">
            <el-input v-model="registerForm.nickname" placeholder="昵称" :prefix-icon="UserFilled"
              size="large"></el-input>
          </el-form-item>
          <el-form-item prop="email">
            <el-input v-model="registerForm.email" placeholder="邮箱" :prefix-icon="Message" size="large"></el-input>
          </el-form-item>
          <el-form-item prop="code">
            <div class="code-input-wrapper">
              <el-input v-model="registerForm.code" placeholder="邮箱验证码" :prefix-icon="Key" size="large"></el-input>
              <el-button type="primary" :disabled="codeDisabled" @click="sendCode" size="large">
                {{ codeButtonText }}
              </el-button>
            </div>
          </el-form-item>
          <el-form-item prop="password">
            <el-input v-model="registerForm.password" type="password" placeholder="密码" :prefix-icon="Lock" size="large"
              show-password></el-input>
          </el-form-item>
          <el-form-item prop="confirmPassword">
            <el-input v-model="registerForm.confirmPassword" type="password" placeholder="确认密码" :prefix-icon="Lock"
              size="large" show-password></el-input>
          </el-form-item>
          <el-form-item class="small-margin">
            <el-button type="primary" size="large" @click="handleRegister" class="register-button">注册</el-button>
          </el-form-item>
          <el-form-item class="small-margin">
            <div class="login-link">
              <span>已有账号？</span>
              <el-link type="primary" @click="goToLogin">立即登录</el-link>
            </div>
          </el-form-item>
        </el-form>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { User, UserFilled, Message, Key, Lock } from '@element-plus/icons-vue'
import { userApi } from '@/api'
import { ElMessage } from 'element-plus'

const router = useRouter()

const registerFormRef = ref(null)

const registerForm = reactive({
  username: '',
  nickname: '',
  email: '2107885241@qq.com',
  code: '',
  password: '',
  confirmPassword: ''
})

const codeDisabled = ref(false)
const countdown = ref(60)
const codeButtonText = ref('获取验证码')

const validatePassword = (rule, value, callback) => {
  if (value === '') {
    callback(new Error('请输入密码'))
  } else if (value.length < 6) {
    callback(new Error('密码长度不能少于6位'))
  } else {
    if (registerForm.confirmPassword !== '') {
      registerFormRef.value.validateField('confirmPassword')
    }
    callback()
  }
}

const validateConfirmPassword = (rule, value, callback) => {
  if (value === '') {
    callback(new Error('请再次输入密码'))
  } else if (value !== registerForm.password) {
    callback(new Error('两次输入的密码不一致'))
  } else {
    callback()
  }
}

const rules = {
  username: [
    { required: true, message: '请输入用户名', trigger: 'blur' },
    { min: 3, max: 20, message: '用户名长度在 3 到 20 个字符', trigger: 'blur' }
  ],
  nickname: [
    { required: true, message: '请输入昵称', trigger: 'blur' },
    { min: 2, max: 20, message: '昵称长度在 2 到 20 个字符', trigger: 'blur' }
  ],
  email: [
    { required: true, message: '请输入邮箱', trigger: 'blur' },
    { type: 'email', message: '请输入正确的邮箱格式', trigger: 'blur' }
  ],
  code: [
    { required: true, message: '请输入验证码', trigger: 'blur' },
    { len: 6, message: '验证码长度为6位', trigger: 'blur' }
  ],
  password: [
    { required: true, validator: validatePassword, trigger: 'blur' }
  ],
  confirmPassword: [
    { required: true, validator: validateConfirmPassword, trigger: 'blur' }
  ]
}

const sendCode = async () => {
  if (!registerForm.email) {
    registerFormRef.value.validateField('email')
    return
  }

  codeDisabled.value = true
  countdown.value = 60
  codeButtonText.value = `${countdown.value}秒后重试`

  try {
    let result = await userApi.sendCode(registerForm.email)
    if(!result.data){
      codeDisabled.value = false
      codeButtonText.value = '获取验证码'
      return ElMessage.warning('邮箱发送失败')
    }
    const timer = setInterval(() => {
      countdown.value--
      if (countdown.value <= 0) {
        clearInterval(timer)
        codeDisabled.value = false
        codeButtonText.value = '获取验证码'
      } else {
        codeButtonText.value = `${countdown.value}秒后重试`
      }
    }, 1000)
  } catch (ex) {
      codeDisabled.value = false
      codeButtonText.value = '获取验证码'
  }
}

const handleRegister = () => {
  registerFormRef.value.validate((valid) => {
    if (valid) {
      console.log('注册信息:', registerForm)
    }
  })
}

const goToLogin = () => {
  router.push('/login')
}
</script>

<style scoped>
.register-container {
  display: flex;
  min-height: 100vh;
}

.register-left {
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

.register-right {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
  background: white;
  padding: 40px;
}

.register-card {
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

.register-form :deep(.el-form-item) {
  margin-bottom: 20px;
}

.register-form :deep(.small-margin) {
  margin-bottom: 12px;
}

.code-input-wrapper {
  display: flex;
  gap: 10px;
}

.code-input-wrapper .el-input {
  flex: 1;
}

.code-input-wrapper .el-button {
  width: 140px;
}

.register-button {
  width: 100%;
}

.login-link {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 8px;
  font-size: 14px;
}

.register-form :deep(.el-input__prefix) {
  color: #333;
}

.register-form :deep(.el-input__icon) {
  color: #333;
}
</style>
