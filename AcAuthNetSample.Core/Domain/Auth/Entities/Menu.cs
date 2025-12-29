using AcAuthNetSample.Core.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.Entities {

    [Table("menu")]
    [Comment("系统菜单表（含目录、菜单、按钮权限）")]
    public class Menu : BaseEntity {
        /// <summary>
        /// 父菜单ID（顶级菜单填0）
        /// </summary>
        [Column("parent_id", TypeName = "int", Order = 1)]
        [Comment("父菜单ID（顶级菜单填0）")]
        [Required(ErrorMessage = "父菜单ID不能为空")] // 必填约束
        public int ParentId { get; set; } = 0; // 默认值：顶级菜单

        /// <summary>
        /// 菜单名称（显示用）
        /// </summary>
        [Column("menu_name", TypeName = "varchar(50)", Order = 2)]
        [Comment("菜单名称（显示用）")]
        [Required(ErrorMessage = "菜单名称不能为空")] // 必填约束
        [MaxLength(50, ErrorMessage = "菜单名称长度不能超过50个字符")]
        public string MenuName { get; set; } = string.Empty; // 移除可空，避免null

        /// <summary>
        /// 菜单类型（1=目录，2=菜单，3=按钮）
        /// </summary>
        [Column("menu_type", TypeName = "varchar(10)", Order = 3)]
        [Comment("菜单类型（1=目录，2=菜单，3=按钮）")]
        [Required(ErrorMessage = "菜单类型不能为空")] // 必填约束
        [MaxLength(10, ErrorMessage = "菜单类型长度不能超过10个字符")]
        [RegularExpression(@"^[123]$", ErrorMessage = "菜单类型只能是1（目录）、2（菜单）、3（按钮）")] // 格式校验
        public string MenuType { get; set; } = string.Empty; // 移除可空

        /// <summary>
        /// 前端路由路径
        /// </summary>
        [Column("path", TypeName = "varchar(200)", Order = 4)]
        [Comment("前端路由路径（菜单类型为2时必填）")]
        [MaxLength(200, ErrorMessage = "路由路径长度不能超过200个字符")]
        public string? Path { get; set; } // 可空（按钮/目录类型无需路径）

        /// <summary>
        /// 前端组件路径
        /// </summary>
        [Column("component", TypeName = "varchar(200)", Order = 5)]
        [Comment("前端组件路径（Vue/React组件地址，菜单类型为2时必填）")]
        [MaxLength(200, ErrorMessage = "组件路径长度不能超过200个字符")]
        public string? Component { get; set; } // 可空（按钮/目录类型无需组件）

        /// <summary>
        /// 权限标识（如system:user:list）
        /// </summary>
        [Column("permission", TypeName = "varchar(100)", Order = 6)]
        [Comment("权限标识（按钮类型必填，格式：模块:功能:操作）")]
        [MaxLength(100, ErrorMessage = "权限标识长度不能超过100个字符")]
        public string? Permission { get; set; } // 可空（目录/菜单类型无需权限标识）

        /// <summary>
        /// 排序号（同级菜单展示顺序）
        /// </summary>
        [Column("sort", TypeName = "int", Order = 7)]
        [Comment("排序号（数字越小越靠前）")]
        [Required(ErrorMessage = "排序号不能为空")] // 必填约束
        [Range(0, int.MaxValue, ErrorMessage = "排序号必须为非负整数")]
        public int Sort { get; set; } = 0; // 默认值：0

        /// <summary>
        /// 是否启用（true=启用，false=禁用）
        /// </summary>
        [Column("is_enabled", TypeName = "bit", Order = 8)]
        [Comment("是否启用（true=启用，false=禁用）")]
        [Required(ErrorMessage = "启用状态不能为空")] // 必填约束
        public bool IsEnabled { get; set; } = true; // 默认值：启用

        /// <summary>
        /// 菜单图标（前端显示）
        /// </summary>
        [Column("icon", TypeName = "varchar(50)", Order = 9)]
        [Comment("菜单图标（如el-icon-user）")]
        [MaxLength(50, ErrorMessage = "图标名称长度不能超过50个字符")]
        public string? Icon { get; set; } // 可空

        /// <summary>
        /// 是否外链（true=是，false=否）
        /// </summary>
        [Column("is_external", TypeName = "bit", Order = 10)]
        [Comment("是否外链（true=是，false=否）")]
        [Required(ErrorMessage = "外链状态不能为空")] // 必填约束
        public bool IsExternal { get; set; } = false; // 默认值：否

        /// <summary>
        /// 是否隐藏菜单（true=隐藏，false=显示）
        /// </summary>
        [Column("is_hide", TypeName = "bit", Order = 11)]
        [Comment("是否隐藏菜单（true=隐藏，false=显示）")]
        [Required(ErrorMessage = "隐藏状态不能为空")] // 必填约束
        public bool IsHide { get; set; } = false; // 默认值：显示

        /// <summary>
        /// 路由重定向地址
        /// </summary>
        [Column("redirect", TypeName = "varchar(200)", Order = 12)]
        [Comment("路由重定向地址（目录类型常用）")]
        [MaxLength(200, ErrorMessage = "重定向地址长度不能超过200个字符")]
        public string? Redirect { get; set; } // 可空

        /// <summary>
        /// 菜单备注/说明
        /// </summary>
        [Column("remark", TypeName = "varchar(500)", Order = 13)]
        [Comment("菜单备注/说明")]
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; } // 可空
    }
}
