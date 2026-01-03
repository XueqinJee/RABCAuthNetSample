using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class RePasswordDto {
        public string? Password { get; set; }
        public string? Repassword { get; set; }
        public int UserId { get; set; }
    }
}
