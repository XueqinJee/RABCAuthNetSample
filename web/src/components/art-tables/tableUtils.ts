
export const defaultResponseAdapter = (response: unknown, deep = 0) => {
    if (!response || deep > 3) {
        return { records: [], total: 0 }
    }
    if (Array.isArray(response)) {
        return { records: response, total: response.length }
    }

    if (typeof response != 'object') {
        console.warn('无法识别的数据格式', response)
        return { records: [], total: 0 }
    }

    const res = response as Record<string, unknown>

    // 处理标准格式或直接列表
    const recordFields = ['records', 'data', 'list', 'items', 'result', 'content']
    let records = []
    for (const field of recordFields) {
        if (field in res && Array.isArray(res[field])) {
            records = res[field]
            break;
        }
    }


    // 若还未找到，尝试寻找第二层级
    if (records.length == 0) {
        for (const field of recordFields) {
            if (field in res && typeof res[field] === 'object') {
                return defaultResponseAdapter(res[field], deep + 1)
            }
        }
    }

    let total = 0
    if ('total' in res && typeof res['total'] === 'number') {
        total = res['total']
    } else if ('totalCount' in res && typeof res['totalCount'] === 'number') {
        total = res['totalCount']
    }


    return { records: records, total: total }
}

export const delayPromiseAsync = (time: number) => {

}