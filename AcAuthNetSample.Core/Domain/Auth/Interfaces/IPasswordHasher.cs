using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.Interfaces {
    public interface IPasswordHasher {
        (string salt, string hashPasswod) HashPassword(string password, string? salt = null);
        bool VerifyPassword(string password, string hash, string salt);
    }
}
