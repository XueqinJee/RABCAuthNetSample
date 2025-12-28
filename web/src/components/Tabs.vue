<template>
  <div class="tabs-container">
    <el-tabs
      :model-value="activeTab"
      type="card"
      closable
      @tab-click="handleTabClick"
      @tab-remove="handleTabRemove"
    >
      <el-tab-pane
        v-for="tab in tabs"
        :key="tab.name"
        :label="tab.label"
        :name="tab.name"
        :closable="tab.closable"
      >
        <template #label>
          <div class="tab-label">
            <el-icon v-if="tab.icon" :size="16" class="tab-icon">
              <component :is="tab.icon" />
            </el-icon>
            <span v-if="tab.showTitle" class="tab-title">{{ tab.title }}</span>
          </div>
        </template>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script setup>
import { HomeFilled, User, Lock, Key, Setting } from '@element-plus/icons-vue'

const props = defineProps({
  tabs: {
    type: Array,
    default: () => []
  },
  activeTab: {
    type: String,
    default: ''
  }
})

const emit = defineEmits(['tab-click', 'tab-remove'])

const handleTabClick = (tab) => {
  const tabName = tab.props.name
  emit('tab-click', tabName)
}

const handleTabRemove = (tabName) => {
  emit('tab-remove', tabName)
}
</script>

<style scoped>
.tabs-container {
  background: white;
  border-bottom: 1px solid #e8e8e8;
  padding: 4px 16px 0;
}

.tabs-container :deep(.el-tabs) {
  --el-tabs-header-height: 40px;
}

.tabs-container :deep(.el-tabs__header) {
  margin: 0;
  border: none;
}

.tabs-container :deep(.el-tabs__nav) {
  border: none;
}

.tabs-container :deep(.el-tabs__item) {
  height: 30px;
  line-height: 30px;
  padding: 0 12px;
  font-size: 13px;
  border: 1px solid #e8e8e8;
  margin-right: 4px;
  border-radius: 4px 4px 0 0;
  background: #f0f0f0;
  color: #666;
  transition: all 0.3s;
}

.tabs-container :deep(.el-tabs__item:hover) {
  background: #e0e0e0;
  color: #333;
}

.tabs-container :deep(.el-tabs__item.is-active) {
  background: white;
  color: #667eea;
  border-bottom-color: white;
  border-top: 3px solid #667eea;
  font-weight: 500;
  box-shadow: 0 -2px 8px rgba(102, 126, 234, 0.15);
}

.tabs-container :deep(.el-tabs__item .el-icon-close) {
  width: 14px;
  height: 14px;
  margin-left: 8px;
  font-size: 12px;
  border-radius: 50%;
  transition: all 0.3s;
}

.tabs-container :deep(.el-tabs__item .el-icon-close:hover) {
  background: #ff4d4f;
  color: white;
}

.tab-label {
  display: flex;
  align-items: center;
  gap: 6px;
}

.tab-icon {
  flex-shrink: 0;
}

.tab-title {
  white-space: nowrap;
}
</style>
