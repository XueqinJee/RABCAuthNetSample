using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AuthNet.Shard.Helpers {
    public static class NamingConventions {
        /// <summary>
        /// 蛇形命名（如 CreateTime → create_time）
        /// </summary>
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            // 匹配大写字母，在前面加下划线，最后转小写
            var result = Regex.Replace(input, "([a-z0-9])([A-Z])", "$1_$2").ToLower();
            // 处理开头下划线（如 ID → id，避免 _id）
            return result.StartsWith("_") ? result[1..] : result;
        }
    }
}
