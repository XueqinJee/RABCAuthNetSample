using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Comments.Dtos {
    public class PageResult<T> {
        /// <summary>
        /// 当前页数据列表
        /// </summary>
        public List<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// 当前页码（从1开始）
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
