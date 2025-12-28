<template>
  <el-select
    v-model="selectedIcon"
    filterable
    placeholder="请选择图标"
    style="width: 100%"
    @change="handleChange"
  >
    <template #prefix>
      <el-icon v-if="selectedIcon" :size="16">
        <component :is="selectedIcon" />
      </el-icon>
    </template>
    <template #default>
      <div class="icon-select-dropdown">
        <div
          v-for="icon in filteredIcons"
          :key="icon"
          class="icon-item"
          :class="{ 'is-selected': selectedIcon === icon }"
          @click="handleSelect(icon)"
        >
          <el-icon :size="20">
            <component :is="icon" />
          </el-icon>
          <span class="icon-name">{{ icon }}</span>
        </div>
      </div>
    </template>
  </el-select>
</template>

<script setup>
import { ref, computed, watch } from 'vue'
import {
  Plus, Edit, Delete, Search
} from '@element-plus/icons-vue'

const props = defineProps({
  modelValue: {
    type: String,
    default: ''
  }
})

const emit = defineEmits(['update:modelValue', 'change'])

const selectedIcon = ref(props.modelValue)

const allIcons = {
  Plus, Edit, Delete, Search
}

const filteredIcons = computed(() => {
  if (!searchQuery.value) {
    return Object.keys(allIcons)
  }
  const query = searchQuery.value.toLowerCase()
  return Object.keys(allIcons).filter(iconName => 
    iconName.toLowerCase().includes(query)
  )
})

const handleSelect = (icon) => {
  selectedIcon.value = icon
  emit('update:modelValue', icon)
  emit('change', icon)
}

const handleChange = (value) => {
  selectedIcon.value = value
  emit('update:modelValue', value)
}

watch(() => props.modelValue, (newVal) => {
  selectedIcon.value = newVal
})
</script>

<style scoped>
.icon-select-dropdown {
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  gap: 10px;
  padding: 10px;
  max-height: 300px;
  overflow-y: auto;
}

.icon-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 10px;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.2s;
}

.icon-item:hover {
  background-color: #f5f5f5;
}

.icon-item.is-selected {
  background-color: #e6f7ff;
  color: #1890ff;
}

.icon-name {
  margin-top: 5px;
  font-size: 12px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  max-width: 80px;
  text-align: center;
}
</style>