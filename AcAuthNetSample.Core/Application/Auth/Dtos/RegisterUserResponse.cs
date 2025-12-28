using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class RegisterUserResponse {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
