<template>
  <div class="dashboard">
    <div class="welcome-card">
      <h1>欢迎使用 AcAuthNet 认证系统</h1>
      <p>这是一个功能完善的权限管理系统</p>
    </div>
    <div class="stats-grid">
      <div class="stat-card">
        <div class="stat-icon user-icon">
          <el-icon :size="32"><User /></el-icon>
        </div>
        <div class="stat-info">
          <div class="stat-value">1,234</div>
          <div class="stat-label">用户总数</div>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon role-icon">
          <el-icon :size="32"><Lock /></el-icon>
        </div>
        <div class="stat-info">
          <div class="stat-value">56</div>
          <div class="stat-label">角色数量</div>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon permission-icon">
          <el-icon :size="32"><Key /></el-icon>
        </div>
        <div class="stat-info">
          <div class="stat-value">128</div>
          <div class="stat-label">权限数量</div>
        </div>
      </div>
      <div class="stat-card">
        <div class="stat-icon online-icon">
          <el-icon :size="32"><Connection /></el-icon>
        </div>
        <div class="stat-info">
          <div class="stat-value">89</div>
          <div class="stat-label">在线用户</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'DashboardView'
}
</script>

<script setup>
import { User, Lock, Key, Connection } from '@element-plus/icons-vue'
</script>

<style scoped>
.dashboard {
  height: 100%;
}

.welcome-card {
  background: white;
  padding: 40px;
  border-radius: 8px;
  margin-bottom: 24px;
  text-align: center;
}

.welcome-card h1 {
  margin: 0 0 12px 0;
  font-size: 28px;
  color: #333;
}

.welcome-card p {
  margin: 0;
  font-size: 16px;
  color: #666;
}

.stats-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
  gap: 24px;
}

.stat-card {
  background: white;
  padding: 24px;
  border-radius: 8px;
  display: flex;
  align-items: center;
  gap: 16px;
}

.stat-icon {
  width: 64px;
  height: 64px;
  border-radius: 12px;
  display: flex;
  align-items: center;
  justify-content: center;
  color: white;
}

.user-icon {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

.role-icon {
  background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
}

.permission-icon {
  background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
}

.online-icon {
  background: linear-gradient(135deg, #43e97b 0%, #38f9d7 100%);
}

.stat-info {
  flex: 1;
}

.stat-value {
  font-size: 28px;
  font-weight: bold;
  color: #333;
  margin-bottom: 4px;
}

.stat-label {
  font-size: 14px;
  color: #666;
}
</style>
