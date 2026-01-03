<template>
    <section class="art-table-container">
        <div class="form">
            <art-form :config="props.config.form"></art-form>
        </div>
        <div class="table">
            <el-table :data="props.config.data">
                <template v-for="item in props.config.header">
                    <el-table-column 
                        v-if="item.type == 'text'"
                        :prop="item.name" 
                        :label="item.label" 
                        :width="item.width"
                        show-overflow-tooltip></el-table-column>
                    
                     <el-table-column 
                        v-if="item.type == 'tag'"
                        :prop="item.name" 
                        :label="item.label" 
                        :width="item.width"
                        show-overflow-tooltip>
                        <template #default="scope">
                            <el-tag :type="item.status">{{ scope.row[item.name] }}</el-tag>
                        </template>
                    </el-table-column>

                    <el-table-column 
                        v-if="item.type == 'switch'"
                        :prop="item.name" 
                        :label="item.label" 
                        :width="item.width"
                        show-overflow-tooltip>
                        <template #default="scope">
                            <el-switch v-model="scope.row[item.name]" :disabled="item.disabled" size="small"></el-switch>
                        </template>
                    </el-table-column>

                    <el-table-column v-if="item.type == 'solt'" :label="item.label" :width="item.width">
                        <template #default="scope">
                            <slot :name="item.name" :row="scope.row" :index="scope.$index"></slot>
                        </template>
                    </el-table-column>
                </template>
            </el-table>
        </div>
    </section>
</template>

<script setup lang="ts">
import type { TableConfig, TableItemConfig } from '@/config/components/art.table.config'
import ArtForm from './ArtForm.vue';

const props = defineProps<{
    config: TableConfig
}>()
</script>