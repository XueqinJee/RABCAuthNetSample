using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class LoginUserRequest {

        [Required(ErrorMessage = "用户名不可为空")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "密码不可为空")]
        public string? Password { get; set; }
    }
}
