using AcAuthNetSample.Core.Domain.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Infrastructure.Repositories.Auth.Interfaces {
    public interface IAccessTokenRepository : IBaseRepository<TokenAccess> {

        Task<TokenAccess?> GetByRefreshTokenAsync(string refreshToken);
        Task<TokenAccess?> GetByUserIdAndClientAsync(int userId, string client);
        Task RevokeTokenAsync(string refreshToken);
        Task RevokeAllUserTokensAsync(int userId);
        Task<TokenAccess> CreateTokenAsync(TokenAccess tokenAccess);
    }
}
