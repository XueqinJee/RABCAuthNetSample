using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class LoginUserResponse {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset ExpireTime { get; set; }
        public string? userName { get; set; }
        public string? NickName { get; set; }
        public string? Avatar { get; set; }
    }
}
