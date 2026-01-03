using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Comments.Dtos {
    public class BasePageQuery {
        /// <summary>
        /// 当前页码（默认1，从1开始）
        /// </summary>
        private int _pageIndex = 1;
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value; // 自动修正非法页码
        }

        /// <summary>
        /// 每页条数（默认10，最小1，最大100）
        /// </summary>
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1) _pageSize = 1;
                else if (value > 100) _pageSize = 100; // 限制最大条数防止性能问题
                else _pageSize = value;
            }
        }
    }
}
