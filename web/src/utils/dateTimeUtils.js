/**
 * 日期格式化工具
 * @param {Date | string | number} date - 要格式化的日期（Date对象/时间戳/日期字符串）
 * @param {string} format - 格式化模板，支持：yyyy-年 MM-月 dd-日 HH-时(24) hh-时(12) mm-分 ss-秒 SSS-毫秒
 * @returns {string} 格式化后的日期字符串
 */
function formatDate(date, format = 'yyyy-MM-dd HH:mm:ss') {
    // 处理入参，转换为标准Date对象
    let targetDate;
    if (date instanceof Date) {
        targetDate = date;
    } else if (typeof date === 'string' || typeof date === 'number') {
        targetDate = new Date(date);
        // 处理无效日期
        if (isNaN(targetDate.getTime())) {
            console.error('无效的日期格式:', date);
            return '';
        }
    } else {
        targetDate = new Date(); // 默认当前时间
    }

    // 定义日期映射
    const fmtObj = {
        y: targetDate.getFullYear(), // 年
        M: targetDate.getMonth() + 1, // 月（0-11 → 1-12）
        d: targetDate.getDate(), // 日
        H: targetDate.getHours(), // 时（24小时制）
        h: targetDate.getHours() % 12 || 12, // 时（12小时制）
        m: targetDate.getMinutes(), // 分
        s: targetDate.getSeconds(), // 秒
        S: targetDate.getMilliseconds() // 毫秒
    };

    // 补零处理：月/日/时/分/秒补两位，毫秒补三位
    const formatNumber = (num, length = 2) => num.toString().padStart(length, '0');

    // 替换模板字符
    return format.replace(/yyyy|MM|dd|HH|hh|mm|ss|SSS/g, (match) => {
        switch (match) {
            case 'yyyy': return fmtObj.y;
            case 'MM': return formatNumber(fmtObj.M);
            case 'dd': return formatNumber(fmtObj.d);
            case 'HH': return formatNumber(fmtObj.H);
            case 'hh': return formatNumber(fmtObj.h);
            case 'mm': return formatNumber(fmtObj.m);
            case 'ss': return formatNumber(fmtObj.s);
            case 'SSS': return formatNumber(fmtObj.S, 3);
            default: return match;
        }
    });
}

export { formatDate }