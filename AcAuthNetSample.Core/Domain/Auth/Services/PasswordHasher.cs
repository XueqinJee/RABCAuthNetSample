using AcAuthNetSample.Core.Domain.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.Services {
    public class PasswordHasher : IPasswordHasher {
        public (string salt, string hashPasswod) HashPassword(string password, string? salt = null)
        {
            var rsas = new Byte[16];
            if (salt == null)
            {
                using (var rsg = RandomNumberGenerator.Create())
                {
                    rsg.GetBytes(rsas);
                }
            }
            else
            {
                rsas = Convert.FromBase64String(salt);
            }

            var passByte = Encoding.UTF8.GetBytes(password);

            int interationCount = 1_0000;
            int hasLength = 64;

            var rfbPwd = Rfc2898DeriveBytes.Pbkdf2(passByte, rsas, interationCount, HashAlgorithmName.SHA256, hasLength);
            return (Convert.ToBase64String(rsas), Convert.ToBase64String(rfbPwd));
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            if (string.IsNullOrEmpty(salt))
                throw new ArgumentException("账户未初始化，Salt 参数为空，无法验证密码");

            var (sa, pwdHash) = HashPassword(password, salt);
            return hash == pwdHash;
        }

    }
}
