export interface FormItemConfig {
    name: string,
    label: string,
    type: 'input' | 'select' | 'checkbox' | 'string',
    inputType?: 'text' | 'number' | 'password',
    source?: Array<any>,
    options?: {
        label: string,
        value: string | number
    },
    defaultValue?: any,
    isRemoteData?: boolean,
    remoteCallFunc?: Function,
    width?: number
}

export interface TableItemConfig {
    name: string,
    label: string,
    width?: number,
    type?: 'text' | 'switch' | 'tag' | 'solt',
    status?: 'primary' | 'success' | 'info' | 'warning' | 'danger'
    disabled?: boolean
}

export interface TableConfig {
    form: Array<FormItemConfig>,
    header: Array<TableItemConfig>,
    rowKey: string,
    pageParam?: {
        page: number,
        pageSize: number
    },
    data: Array<any>,
    apiUrl?: string,
    method?: 'GET' | 'POST'
}