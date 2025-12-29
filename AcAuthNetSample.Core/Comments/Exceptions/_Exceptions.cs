using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Comments.Exceptions {
    /// <summary>
    /// 认证业务异常
    /// </summary>
    public class AuthException : Exception {
        /// <summary>
        /// 错误码（便于前端区分处理）
        /// </summary>
        public int ErrorCode { get; }

        public AuthException(string message, int errorCode = 400) : base(message)
        {
            ErrorCode = errorCode;
        }

        public AuthException(string message, Exception innerException, int errorCode = 400) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }

    /// <summary>
    /// 账户锁定异常
    /// </summary>
    public class AccountLockedException : AuthException {
        public AccountLockedException(string message) : base(message, 403) { }
    }

    /// <summary>
    /// 认证失败异常（密码错误/账户不存在）
    /// </summary>
    public class AuthenticationFailedException : AuthException {
        public AuthenticationFailedException(string message) : base(message, 401) { }
    }

    /// <summary>
    /// 业务参数异常
    /// </summary>
    public class BusinessArgumentException : AuthException {
        public BusinessArgumentException(string message) : base(message, 400) { }
    }
}
