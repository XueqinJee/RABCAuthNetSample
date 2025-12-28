using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.ValueObjects {
    public record Email {
        public string Value { get; set; }
        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("邮箱不能为空");

            if (!IsValidEmail(email))
                throw new ArgumentException("邮箱格式不正确");

            return new Email(email);
        }

        private static bool IsValidEmail(string email)
        {
            return email.Contains("@");
        }
    }
}
