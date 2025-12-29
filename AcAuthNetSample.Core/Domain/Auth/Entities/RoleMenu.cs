using AcAuthNetSample.Core.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.Entities {
    /// <summary>
    /// 角色-菜单关联表（多对多）
    /// </summary>
    [Table("role_menu")]
    [Comment("角色菜单关联表（多对多）")]
    public class RoleMenu : BaseEntity {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Column("role_id", Order = 1)]
        [Comment("角色ID")]
        [Required(ErrorMessage = "角色ID不能为空")]
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [Column("menu_id", Order = 2)]
        [Comment("菜单ID")]
        [Required(ErrorMessage = "菜单ID不能为空")]
        [ForeignKey(nameof(Menu))]
        public int MenuId { get; set; }

        public Role? Role { get; set; }

        public Menu? Menu { get; set; }
    }
}
