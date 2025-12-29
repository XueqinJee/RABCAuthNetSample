using AcAuthNetSample.Core.Domain.Auth.Entities;
using AcAuthNetSample.Core.Infrastructure.Data;
using AcAuthNetSample.Core.Infrastructure.Repositories.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcAuthNetSample.Core.Infrastructure.Repositories.Auth {
    public class AccessTokenRepository : BaseRepository<TokenAccess>, IAccessTokenRepository {
        private readonly ApplicationDbContext _context;

        public AccessTokenRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TokenAccess> CreateOrUpdateTokenAsync(TokenAccess tokenAccess)
        {
            // 查询Token是否存在，若存在更新，反之创建
            var token = _context.AccessTokens.FirstOrDefault(x => x.UserId == tokenAccess.UserId 
                                                                    && x.Client == tokenAccess.Client);

            if(token != null)
            {
                token.Token = tokenAccess.Token;
                token.RefreshToken = tokenAccess.RefreshToken;
                token.FailCount = tokenAccess.FailCount;
                token.ExpireTime = tokenAccess.ExpireTime;

                _context.AccessTokens.Update(token);
            }
            else
            {
                await _context.AccessTokens.AddAsync(tokenAccess);

            } 
            await _context.SaveChangesAsync();
            return tokenAccess;
        }

        public async Task<TokenAccess?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.AccessTokens
                            .Include(x => x.User)
                            .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken && x.ExpireTime > DateTime.UtcNow);
        }

        public async Task<TokenAccess?> GetByUserIdAndClientAsync(int userId, string client)
        {
            return await _context.AccessTokens
                            .FirstOrDefaultAsync(x => x.UserId == userId && x.Client == client);
        }

        public async Task RevokeAllUserTokensAsync(int userId)
        {
            var tokens = await _context.AccessTokens
                                .Where(x => x.UserId == userId)
                                .ToListAsync();

            _context.AccessTokens.RemoveRange(tokens);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeTokenAsync(string refreshToken)
        {
            var token = await GetByRefreshTokenAsync(refreshToken);
            if (token != null)
            {
                _context.AccessTokens.Remove(token);
                await _context.SaveChangesAsync();
            }
        }
    }
}
