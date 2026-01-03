using AcAuthNetSample.Core.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.Entities {
    [Table("user")]
    public class User : BaseEntity {

        [Required]
        [Column(TypeName = "varchar(32)")]
        [Comment("昵称")]
        public string? NickName { get; set; }

        [Required]
        [Column(TypeName = "varchar(32)")]
        [Comment("用户名")]
        public string? UserName { get; private set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        public string? Password { get; private set; }

        [Column(TypeName = "varchar(64)")]
        [Comment("密钥的盐")]
        public string? Salt { get; private set; }

        [Column(TypeName = "varchar(256)")]
        [Comment("头像")]
        public string? Avator { get;  set; }

        [Column(TypeName = "varchar(32)")]
        [Comment("邮箱")]
        public string? Email { get;  set; }

        [Column(TypeName = "varchar(15)")]
        [Comment("手机号")]
        public string? Phone { get;  set; }

        [Column(TypeName = "varchar(200)")]
        [Comment("描述")]
        public string? Description { get;  set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public bool IsDisabled { get; set; }

        public User() { }

        public static User Create(string userName, string email)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("用户名不能为空");

            var user = new User
            {
                UserName = userName,
                Email = email,
            };
            return user;
        }

        public void SetPassword(string salt, string password)
        {
            this.Salt = salt;
            this.Password = password;
        }

        public Role? Role { get; set; }
    }
}
