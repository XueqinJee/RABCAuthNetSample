using AcAuthNetSample.Core.Application.Auth.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Interfaces {
    public interface IAuthTokenService {
        Task<LoginUserResponse> CreateUserToken(string userName, string userAgent, string client, string ip);

        Task<LoginUserResponse> RefreshToekn(LoginUserResponse  loginUserResponse);
    }
}
