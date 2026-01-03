import { ref, computed, onMounted, reactive } from 'vue'
import { defaultResponseAdapter } from './tableUtils'

export interface ColumnOption {
    type?: 'text' | 'tag' | 'switch' | 'date',
    prop?: string,
    label?: string,
    width?: number | string,
    fixed?: Boolean,
    hide?: Boolean,
    disabled?: Boolean,
    formatter?: string,
    useSlot?: Boolean,
    slotName?: string,
    status?: 'primary' | 'danger' | 'warning',
}

export interface FormFieldOption {
    name: string,
    label: string,
    type: 'input' | 'select' | 'checkbox' | 'string',
    inputType?: 'text' | 'number' | 'password',
    source?: Array<any>,
    options?: {
        label: string,
        value: string | number
    },
    // 远程数据字段映射配置
    remoteFieldMapping?: {
        label: string,    // 显示文本字段名
        value: string     // 值字段名
    },
    defaultValue?: any,
    isRemoteData?: boolean,
    remoteCallFunc?: Function,
    width?: number,
    loading?: boolean
}

export interface PageinationOption{
    current: number,
    size: number,
    total: number
}

export interface UseTableConfig {
    core: {
        apiFn: (params: any) => Promise<any>
        apiParams?: Partial<any>,
        immediate?: boolean,
        columnsFactory?: () => ColumnOption[],
        formFactory?: () => FormFieldOption[],
        paginationKey?: {
            current?: string,
            size?: string
        }
    },
    // 数据处理
    // 周期钩子
    hooks?: {
        onSUccess?: (data: Array<any>) => void
        onError?: (error: any) => void,
        resetFormCallback?: () => void
    }
}

/**
 * 表格数据管理，提供数据获取，分页控制，搜索功能
 * @param config 
 */
export function useTable(config: UseTableConfig) {
    const {
        core: {
            apiFn,
            apiParams = {} as Partial<any>,
            immediate = true,
            columnsFactory,
            formFactory,
            paginationKey = { current: 'pageIndex', size: 'pageSize' }
        },
        hooks: { onSUccess, onError, resetFormCallback } = {}
    } = config

    // 分页字段配置
    const pageKey = paginationKey?.current || 'pageIndex'
    const sizeKey = paginationKey?.size || 'pageSize'

    // 列配置
    const columns: ColumnOption[] = columnsFactory()

    // 表单配置
    const formFields: FormFieldOption[] = formFactory()

    // 加载状态
    const loading = ref(true)
    // 表格数据
    const data = ref<any[]>()
    // 搜索参数
    const searchParams = reactive(Object.assign(
        {
            [pageKey]: 1,
            [sizeKey]: 10
        },
        apiParams || {}
    ))

    // 分页配置
    const pageination = reactive<PageinationOption>({
        current: searchParams[pageKey] || 1,
        size: searchParams[sizeKey] || 10,
        total: 0
    })

    // 是否有数据
    const hasData = computed(() => data.value.length > 0)

    let timer = null;
    const fetchData = async (params: any) => {
        loading.value = true

        if (timer) clearTimeout(timer)

        timer = setTimeout(async () => {
            try {
                searchParams[pageKey] = pageination.current
                searchParams[sizeKey] = pageination.size
                const requestParams = Object.assign({}, searchParams, params || {})
                const response = await apiFn(requestParams)
                // 数据格式转换
                var standardResponse = defaultResponseAdapter(response)
                // 更新数据状态
                data.value = standardResponse.records
                pageination.total = standardResponse.total
                return standardResponse
            } catch (ex) {
                data.value = []
                pageination.total = 0
                throw ex
            } finally {
                loading.value = false;
            }
        }, 400);
    }

    // 获取数据
    const getData = async (params?: any) => {
        return await fetchData(params)
    }

    // 处理分页大小变化
    const handleSizeChange = async (newSize: number) => {
        if (newSize < 0) return
        pageination.size = newSize
        debugger
        await getData();
    }

    // 页码变化
    const handleCurrentChange = async (newCurrent: number) => {
        if (newCurrent < 0) return
        pageination.current = newCurrent

        await getData();
    }

    if (immediate) getData();

    return {
        tableData: data,
        isLoading: loading,
        onPageSizeChange: handleSizeChange,
        onCurrentPageChange: handleCurrentChange,
        loadData: getData,
        // 列以及表单配置
        columns,
        formFields,

        pageination
    }
}