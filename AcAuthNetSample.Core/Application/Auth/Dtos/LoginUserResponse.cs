using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class LoginUserResponse {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpireTime { get; set; }
        public string? userName { get; set; }
    }
}
