using AcAuthNetSample.Core.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.Entities {
    /// <summary>
    /// 系统角色实体
    /// </summary>
    [Table("role")]
    [Comment("系统角色表（用于权限分配）")] // EF Core 7.0+ 支持表级Comment注解
    public class Role : BaseEntity {
        /// <summary>
        /// 角色名称（显示用）
        /// </summary>
        [Column("role_name", TypeName = "varchar(50)", Order = 1)]
        [Comment("角色名称（如超级管理员、普通用户）")]
        [Required(ErrorMessage = "角色名称不能为空")]
        [MaxLength(50, ErrorMessage = "角色名称长度不能超过50个字符")]
        public string RoleName { get; set; } = string.Empty; // 非空+默认值

        /// <summary>
        /// 角色编码（唯一标识，如SUPER_ADMIN、USER）
        /// </summary>
        [Column("role_code", TypeName = "varchar(50)", Order = 2)]
        [Comment("角色编码（全局唯一，用于权限校验）")]
        [Required(ErrorMessage = "角色编码不能为空")]
        [MaxLength(50, ErrorMessage = "角色编码长度不能超过50个字符")]
        [RegularExpression(@"^[A-Z][A-Z0-9_]*$", ErrorMessage = "角色编码仅支持大写字母、数字、下划线，且以字母开头")]
        public string RoleCode { get; set; } = string.Empty; // 非空+默认值

        /// <summary>
        /// 角色描述/备注
        /// </summary>
        [Column("description", TypeName = "varchar(500)", Order = 3)]
        [Comment("角色描述/备注（说明角色用途）")]
        [MaxLength(500, ErrorMessage = "角色描述长度不能超过500个字符")]
        public string? Description { get; set; } // 可空

        /// <summary>
        /// 是否系统内置角色（true=是，false=否）
        /// </summary>
        [Column("is_system", TypeName = "bit", Order = 4)]
        [Comment("是否系统内置角色（内置角色不可删除/修改编码）")]
        [Required(ErrorMessage = "系统内置标识不能为空")]
        public bool IsSystem { get; set; } = false; // 默认非系统内置

        /// <summary>
        /// 是否启用（true=启用，false=禁用）
        /// </summary>
        [Column("is_enabled", TypeName = "bit", Order = 5)]
        [Comment("是否启用（禁用后角色关联的权限失效）")]
        [Required(ErrorMessage = "角色启用状态不能为空")]
        public bool IsEnabled { get; set; } = true; // 默认启用

        /// <summary>
        /// 排序号（角色展示顺序）
        /// </summary>
        [Column("sort", TypeName = "int", Order = 6)]
        [Comment("排序号（数字越小越靠前）")]
        [Required(ErrorMessage = "排序号不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "排序号必须为非负整数")]
        public int Sort { get; set; } = 0; // 默认值0
    }
}
