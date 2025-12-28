using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcAuthNetSample.Core.Application.Configuration.Options {
    public class JwtConfigOptions {
        public static readonly string Name = "JwtConfig";

        private static Encoding DefaultEncoding = Encoding.UTF8;
        private static double DefaultExpireMinute = 30d;

        [Required]
        public string? Issuer { get; set; }

        [Required]
        public string? Audience { get; set; }

        [Required]
        [MinLength(32, ErrorMessage = "密钥长度最少32位")]
        public string? SymmetricSecurityStringKey { get; set; }
        public Encoding Encoding { get; set; } = DefaultEncoding;
        public double Expire { get; set; } = DefaultExpireMinute;

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(this.Encoding.GetBytes(SymmetricSecurityStringKey!));
    }
}
