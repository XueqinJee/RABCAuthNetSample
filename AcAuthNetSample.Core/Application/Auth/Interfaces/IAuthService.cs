using AcAuthNetSample.Core.Application.Auth.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Interfaces {
    public interface IAuthService {

        Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request);

        Task<LoginUserResponse> LoginAsync(string username, string password, string userAgent, string client, string ip);

        Task Logout(string userId, string refreshToken);
    }
}
