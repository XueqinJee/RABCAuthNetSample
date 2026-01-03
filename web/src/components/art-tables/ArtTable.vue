<template>
    <section class="art-table-container">
        <div class="form">
            <art-form :config="props.forms" @on-search="onSearch"></art-form>
        </div>
        <div class="table" v-loading="props.loading">
            <div class="container">
                <el-table :data="props.data" style="height: 90%;">
                    <template v-for="item in props.colums">
                        <el-table-column v-if="item.type == 'text'" :prop="item.prop" :label="item.label"
                            :width="item.width" show-overflow-tooltip></el-table-column>

                        <el-table-column v-else-if="item.type == 'tag'" :prop="item.prop" :label="item.label"
                            :width="item.width" show-overflow-tooltip>
                            <template #default="scope">
                                <el-tag :type="item.status">{{ scope.row[item.prop] }}</el-tag>
                            </template>
                        </el-table-column>

                        <el-table-column v-else-if="item.type == 'switch'" :prop="item.prop" :label="item.label"
                            :width="item.width" show-overflow-tooltip>
                            <template #default="scope">
                                <el-switch v-model="scope.row[item.prop]" :disabled="item.disabled"
                                    size="small"></el-switch>
                            </template>
                        </el-table-column>
                        <el-table-column v-else-if="item.type == 'date'" :prop="item.prop" :label="item.label"
                            :width="item.width" show-overflow-tooltip>
                            <template #default="scope">
                                <el-tag :type="item.status">{{ dateTimeFormatter(scope.row[item.prop], item.formatter)
                                    }}</el-tag>
                            </template>
                        </el-table-column>

                        <el-table-column v-else-if="item.useSlot" :label="item.label" :width="item.width">
                            <template #default="scope">
                                <slot :name="item.prop" :row="scope.row" :index="scope.$index"></slot>
                            </template>
                        </el-table-column>
                    </template>
                </el-table>
                <el-pagination v-model:current-page="props.page.current" v-model:page-size="props.page.size"
                    :total="props.page.total" :page-sizes="[1, 10, 20, 50, 100, 120]"
                    layout="total, sizes,prev, pager, next" @current-change="onCurrentChangeHandle"
                    @size-change="onPageSizeHandle"></el-pagination>
            </div>
        </div>
    </section>
</template>

<script setup lang="ts">
import ArtForm from './ArtForm.vue';
import { ColumnOption, FormFieldOption, PageinationOption } from './useTable';
import { formatDate } from '@/utils/dateTimeUtils'
import { ref, watchEffect } from 'vue'

export interface ArtTableConfig {
    forms: FormFieldOption[],
    colums: ColumnOption[],
    data: any,
    loading: Boolean,
    page: PageinationOption
}

const props = defineProps<ArtTableConfig>()
const emits = defineEmits(['onSearchHandle', 'onCurrentChangeHandle', 'onPageSizeChangeHandle'])

watchEffect(() => {
    console.log(props.page);
})

/**
 * 搜索
 * @param params 
 */
const onSearch = (params) => {
    emits('onSearchHandle', params)
}

const onPageSizeHandle = (data) => {
    emits('onPageSizeChangeHandle', data)
}

const onCurrentChangeHandle = (data) => {
    emits('onCurrentChangeHandle', data)
}

const dateTimeFormatter = (date: string, formatter: string) => {
    if (!formatter) formatter = 'yyyy-MM-dd HH:mm:ss'
    if (date) {
        return formatDate(date, formatter)
    }
    return ''
}
</script>


<style scoped lang="less">
.art-table-container {
    display: flex;
    flex-direction: column;
    height: 100%;

    .table {
        flex: 1;

        .container{
            display: flex;
            flex-direction: column;
            height: 100%;
            .el-table{
                flex: 1;
            }

            .el-pagination{
                margin-top: 15px;
            }
        }
    }
}
</style>