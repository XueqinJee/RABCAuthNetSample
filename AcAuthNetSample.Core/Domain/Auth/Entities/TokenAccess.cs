using AcAuthNetSample.Core.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.Entities {
    [Table("access_token")]
    public class TokenAccess : BaseEntity{

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Column(TypeName = "varchar(128)")]
        [Required]
        [Comment("登录设备")]
        public string? UserAgent { get; set; }

        [Required]
        [Column(TypeName = "varchar(32)")]
        [Comment("登录平台，Android、IOS、Windows等")]
        public string? Client { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        [Comment("登录IP")]
        public string? Ip { get; set; }

        [Column(TypeName = "varchar(max)")]
        [Comment("Token")]
        public string? Token { get; set; }

        [Column(TypeName = "varchar(max)")]
        [Comment("RefreshToken")]
        public string? RefreshToken { get; set; }

        [Comment("失败次数")]
        public int FailCount { get; set; } = 0;

        [Comment("登录时间")]
        public DateTimeOffset LoginOn { get; set; }

        [Comment("登录过期时间")]
        public DateTimeOffset ExpireTime { get; set; }

        public User? User { get; set; }
    }
}
