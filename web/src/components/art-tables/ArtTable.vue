<template>
    <section class="art-table-container">
        <div class="form">
            <art-form :config="props.config.form"></art-form>
        </div>
        <div class="table">
            <el-table>
                <el-form :inline="true">
                    <el-form-item label="用户名">
                        <el-input placeholder="请输入用户名"/>
                    </el-form-item>
                </el-form>
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