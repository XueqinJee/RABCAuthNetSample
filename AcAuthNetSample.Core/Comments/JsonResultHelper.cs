using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AcAuthNetSample.Core.Comments {
    /// <summary>
    /// Web API JsonResult 帮助类（统一返回格式）
    /// </summary>
    public static class JsonResultHelper {
        #region 通用返回模型
        /// <summary>
        /// 统一JSON返回结构
        /// </summary>
        private class ApiResponse<T> {

            public bool IsOk { get; set; } = true;

            /// <summary>
            /// 业务状态码（200=成功，其他=异常）
            /// </summary>
            public int Code { get; set; }

            /// <summary>
            /// 提示信息
            /// </summary>
            public string Msg { get; set; } = string.Empty;

            /// <summary>
            /// 响应数据
            /// </summary>
            public T Data { get; set; } = default!;

            /// <summary>
            /// 时间戳（UTC+8）
            /// </summary>
            public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
        }
        #endregion

        #region 核心配置（解决中文乱码、序列化问题）
        /// <summary>
        /// 获取JSON序列化设置（兼容.NET Framework/.NET Core）
        /// </summary>
        private static object GetJsonSettings()
        {
            return new JsonSerializerOptions
            {
                // 中文不转义
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
                // 忽略null值
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                // 小驼峰命名
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                // 日期格式
                WriteIndented = false
            };
        }
        #endregion

        #region 通用构建方法
        /// <summary>
        /// 构建JsonResult
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="code">业务码</param>
        /// <param name="msg">提示信息</param>
        /// <param name="data">数据</param>
        /// <param name="statusCode">HTTP状态码</param>
        /// <returns>JsonResult</returns>
        private static IActionResult BuildResult<T>(bool isOk ,int code, string msg, T data, int statusCode = 200)
        {
            var response = new ApiResponse<T>
            {
                IsOk = isOk,
                Code = code,
                Msg = msg,
                Data = data,
                Timestamp = DateTime.Now
            };

            // .NET Core Web API
            var jsonResult = new JsonResult(response)
            {
                StatusCode = statusCode
            };
            return jsonResult;
        }
        #endregion

        #region 快捷方法（成功/警告/错误等）
        /// <summary>
        /// 成功响应（默认200）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">返回数据</param>
        /// <param name="msg">提示信息</param>
        /// <returns>JsonResult</returns>
        public static IActionResult Success<T>(T data, string msg = "操作成功")
        {
            return BuildResult(true ,200, msg, data, 200);
        }

        /// <summary>
        /// 成功响应（无数据）
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns>JsonResult</returns>
        public static IActionResult Success(string msg = "操作成功")
        {
            return BuildResult(true, 200, msg, string.Empty, 200);
        }

        /// <summary>
        /// 警告响应（默认200业务码，提示性警告）
        /// </summary>
        /// <param name="msg">警告信息</param>
        /// <param name="data">附加数据</param>
        /// <returns>JsonResult</returns>
        public static IActionResult Warning(string msg = "操作提醒", object data = null)
        {
            return BuildResult(true, 201, msg, data ?? string.Empty, 200);
        }

        /// <summary>
        /// 未授权（401）
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns>JsonResult</returns>
        public static IActionResult Unauthorized(string msg = "未登录或登录已过期")
        {
            return BuildResult(false, 401, msg, string.Empty, 401);
        }

        /// <summary>
        /// 禁止访问（403）
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns>JsonResult</returns>
        public static IActionResult Forbidden(string msg = "无权限访问该资源")
        {
            return BuildResult(false, 403, msg, string.Empty, 403);
        }

        /// <summary>
        /// 参数错误（400）
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="data">错误详情（如参数列表）</param>
        /// <returns>JsonResult</returns>
        public static IActionResult ParamError(string msg = "参数格式错误", object data = null)
        {
            return BuildResult(false, 400, msg, data ?? string.Empty, 400);
        }

        /// <summary>
        /// 资源不存在（404）
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns>JsonResult</returns>
        public static IActionResult NotFound(string msg = "请求的资源不存在")
        {
            return BuildResult(false, 404, msg, string.Empty, 404);
        }

        /// <summary>
        /// 服务器异常（500）
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="data">异常详情（生产环境建议隐藏）</param>
        /// <returns>JsonResult</returns>
        public static IActionResult ServerError(string msg = "服务器内部错误", object data = null)
        {
            return BuildResult(false, 500, msg, data ?? string.Empty, 500);
        }
        #endregion
    }
}
