using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class RegisterUserRequest {

        [Required(ErrorMessage = "昵称不可为空")]
        public string? NickName { get; set; }

        [Required(ErrorMessage = "用户名不可为空")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "密码不可为空")]
        [MinLength(5)]
        [MaxLength(25)]
        public string? Password { get; init; }

        [Required(ErrorMessage = "邮箱不可为空")]
        [EmailAddress(ErrorMessage = "邮箱格式错误")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "验证码不可为空")]
        public string? Code { get; set; }

        [Required]
        public string? ConfirmPassword { get; set; }
    }
}
