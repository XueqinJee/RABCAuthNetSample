using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class RegisterUserRequest {

        [Required]
        public string? UserName { get; init; }

        [Required]
        public string? Password { get; init; }

        [Required]
        public string? Email { get; set; }
    }
}
