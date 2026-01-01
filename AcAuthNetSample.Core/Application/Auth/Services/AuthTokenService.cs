using AcAuthNetSample.Core.Application.Auth.Dtos;
using AcAuthNetSample.Core.Application.Auth.Interfaces;
using AcAuthNetSample.Core.Application.Configuration.Options;
using AcAuthNetSample.Core.Domain.Auth.Entities;
using AcAuthNetSample.Core.Infrastructure.Repositories.Auth.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Services {
    public class AuthTokenService : IAuthTokenService {

        private readonly IDistributedCache _distributeCache;
        private readonly IAccessTokenRepository _accessTokenRepository;
        private readonly IUserRepository _userRepository;

        private readonly ILogger<AuthService> _logger;
        private readonly JwtBearerOptions _jwtBearerOptions;
        private readonly JwtConfigOptions _jwtConfigOptions;

        private readonly TimeSpan _expiresIn = TimeSpan.FromMinutes(200);

        public AuthTokenService(
            ILogger<AuthService> logger,
            IOptionsSnapshot<JwtBearerOptions> jwtBearerOptions,
            IOptionsSnapshot<JwtConfigOptions> jwtConfigOptions,
            IAccessTokenRepository accessTokenRepository,
            IUserRepository userRepository,
            IDistributedCache distributeCache)
        {
            _logger = logger;
            _jwtBearerOptions = jwtBearerOptions.Get(JwtBearerDefaults.AuthenticationScheme);
            _jwtConfigOptions = jwtConfigOptions.Value;
            _accessTokenRepository = accessTokenRepository;
            _userRepository = userRepository;
            _distributeCache = distributeCache;
        }

        public async Task<LoginUserResponse> CreateUserToken(string userName, string userAgent, string client, string ip)
        {
            var (refreshTokenId, refreshToken) = await CreateRefreshToken(userName);

            // 创建Token
            var jwtSecurityDescroptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier, refreshTokenId),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Issuer = _jwtConfigOptions.Issuer,
                Audience = _jwtConfigOptions.Audience,
                Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(20))
            };


            // 获取Handle
            var handle = _jwtBearerOptions.TokenHandlers.OfType<JwtSecurityTokenHandler>().FirstOrDefault() ?? new JwtSecurityTokenHandler();
            var securityToken = handle.CreateJwtSecurityToken(jwtSecurityDescroptor);
            var token = handle.WriteToken(securityToken);

            var user = await _userRepository.GetByUserNameAsync(userName);
            var tokenAccess = new TokenAccess
            {
                UserId = user!.Id,
                UserAgent = userAgent,
                Client = client,
                Ip = ip,
                Token = token,
                RefreshToken = refreshToken,
                LoginOn = DateTimeOffset.UtcNow,
                ExpireTime = DateTimeOffset.UtcNow.Add(_expiresIn),
                FailCount = 0
            };
            await _accessTokenRepository.CreateOrUpdateTokenAsync(tokenAccess);

            return new LoginUserResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpireTime = DateTimeOffset.UtcNow.Add(_expiresIn),
                userName = userName,
                NickName = user.NickName,
                Avatar = user.Avator
            };
        }

        public async Task<LoginUserResponse> RefreshToekn(LoginUserResponse loginUserResponse)
        {
            var tokenParameter = _jwtBearerOptions.TokenValidationParameters.Clone();
            tokenParameter.ValidateLifetime = false;

            ClaimsPrincipal principal;
            try
            {
                var handle = _jwtBearerOptions.TokenHandlers.OfType<JwtSecurityTokenHandler>().FirstOrDefault() ?? new JwtSecurityTokenHandler();
                principal = handle.ValidateToken(loginUserResponse.Token, tokenParameter, out _);
            }catch(Exception ex)
            {
                throw new Exception("Invalid Token");
            }

            // 验证Token是否一致
            var refreshTokenId = principal.Identities.First().Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value;
            var refreshToken = await _distributeCache.GetStringAsync(refreshTokenId);

            if(refreshToken != loginUserResponse.RefreshToken)
            {
                throw new Exception("Invalid Token");
            }

            var tokenAcess = await _accessTokenRepository.GetByRefreshTokenAsync(refreshToken!);
            if(tokenAcess == null)
            {
                throw new Exception("Invalid RefreshToken");
            }

            return await CreateUserToken(loginUserResponse.userName!, tokenAcess.UserAgent!, tokenAcess.Client!, tokenAcess.Ip!);
        }


        private async Task<(string, string)> CreateRefreshToken(string userName)
        {
            var tokenKey = GetTokenKey(userName);

            var rsb = new byte[16];
            using (var rdm = RandomNumberGenerator.Create())
            {
                rdm.GetBytes(rsb);
            }

            var refreshToken = Convert.ToBase64String(rsb);
            await _distributeCache.SetAsync(tokenKey, Encoding.UTF8.GetBytes(refreshToken));
            return (tokenKey, refreshToken);
        }


        private string GetTokenKey(string username)
        {
            return $"{username}-{Guid.NewGuid().ToString("N")}";
        }
    }
}
