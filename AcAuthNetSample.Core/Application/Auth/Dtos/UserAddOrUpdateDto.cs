using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class UserAddOrUpdateDto {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? NickName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool IsDisabled { get; set; }
    }
}
