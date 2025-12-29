using AcAuthNetSample.Core.Application.Auth.Dtos;
using AcAuthNetSample.Core.Application.Auth.Interfaces;
using AcAuthNetSample.Core.Comments.Exceptions;
using AcAuthNetSample.Core.Domain.Auth.Entities;
using AcAuthNetSample.Core.Domain.Auth.Interfaces;
using AcAuthNetSample.Core.Infrastructure.Repositories.Auth.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AcAuthNetSample.Core.Application.Auth.Services {
    public class AuthService : IAuthService {

        private readonly IUserRegistractionService _userRegistractionService;
        private readonly IUserRepository _userRespository;
        private readonly IAccessTokenRepository _accessTokenRepository;
        private readonly IAuthTokenService _authTokenService;
        private readonly IPasswordHasher _passwordHasher;

        private readonly ILogger<AuthService> _logger;

        private static readonly TimeZoneInfo _chinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");

        public AuthService(IUserRegistractionService userRegistractionService,
            IUserRepository userRespository,
            IAuthTokenService authTokenService,
            IPasswordHasher passwordHasher,
            IAccessTokenRepository accessTokenRepository,
            ILogger<AuthService> logger)
        {
            _userRegistractionService = userRegistractionService;
            _userRespository = userRespository;
            _authTokenService = authTokenService;
            _passwordHasher = passwordHasher;
            _accessTokenRepository = accessTokenRepository;
            _logger = logger;
        }

        public async Task<LoginUserResponse> LoginAsync(string username, string password, string userAgent, string client, string ip)
        {
            var hasUser = await _userRegistractionService.IsUserNameExistsAsync(username);
            if (!hasUser)
            {
                throw new Exception("账户不存在");
            }

            var user = await _userRespository.GetByUserNameAsync(username);
            // 校验密码
            await ValidLoginUser(user!, password, userAgent, client, ip);

            // 创建Token
            var model = await _authTokenService.CreateUserToken(user!.UserName!, userAgent, client, ip);
            // 数据库存储
            return model;
        }

        public async Task Logout(string userId, string refreshToken)
        {
            await _accessTokenRepository.RevokeTokenAsync(refreshToken);
        }

        public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request)
        {
            var result = await _userRegistractionService.RegisterAsync(request.NickName!,request.UserName!, request.Email!, request.Password!);

            // 保存到数据库
            await _userRespository.SaveChangeAsync();

            return new RegisterUserResponse
            {
                UserId = result.Id,
                Email = result.Email,
                UserName = result.UserName,
                CreateOn = result.CreateOn
            };
        }

        private async Task ValidLoginUser(User user, string password, string userAgent, string client, string ip)
        {
            var loginToken = await _accessTokenRepository.GetByUserIdAndClientAsync(user.Id, client) 
                                ?? 
                                new TokenAccess()
                                {
                                    UserId = user.Id,
                                    Ip = ip,
                                    UserAgent = userAgent,
                                    Client = client,
                                    FailCount = 0,
                                    ExpireTime = DateTime.UtcNow,
                                    CreateOn = DateTime.UtcNow,
                                };

            bool hasBusinessException = false;
            string exceptionMessage = string.Empty;

            try
            {
                var currentUtcTime = DateTime.UtcNow;
                if(loginToken.FailCount > 5 && loginToken.ExpireTime > currentUtcTime)
                {
                    var unlockTime = TimeZoneInfo.ConvertTimeFromUtc(loginToken.ExpireTime, _chinaTimeZone);
                    throw new AccountLockedException($"账户已被锁定，请在 {unlockTime.ToString("yyyy-MM-ddTHH:mm:ss")} 后再次尝试！");
                }

                if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Salt)) { 
                    hasBusinessException = true;
                    throw new AuthException("用户密码信息异常，请联系管理员！");
                }

                var validPwd = _passwordHasher.VerifyPassword(password, user.Password!, user.Salt!);
                if (!validPwd)
                {
                    loginToken.FailCount += 1;
                    if(loginToken.FailCount > 5)
                    {
                        loginToken.ExpireTime = DateTime.UtcNow.AddMinutes(30);
                    }

                    hasBusinessException = true;
                    throw new AuthenticationFailedException("账号或密码错误！");
                }
                loginToken.FailCount = 0;
                loginToken.ExpireTime = currentUtcTime;
            }
            finally
            {
                await _accessTokenRepository.CreateOrUpdateTokenAsync(loginToken);

                _logger.LogInformation(
                "用户登录令牌更新完成，用户ID：{UserId}，客户端：{Client}，失败次数：{FailCount}，是否锁定：{IsLocked}，异常信息：{ExceptionMessage}",
                user.Id, client, loginToken.FailCount, loginToken.FailCount > 5 && loginToken.ExpireTime > DateTime.UtcNow,
                hasBusinessException ? exceptionMessage : "无");
            }
        }
    }
}
