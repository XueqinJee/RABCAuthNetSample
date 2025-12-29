using AcAuthNetSample.Core.Application.Auth.Dtos;
using AcAuthNetSample.Core.Application.Auth.Interfaces;
using AcAuthNetSample.Core.Domain.Auth.Entities;
using AcAuthNetSample.Core.Domain.Auth.Interfaces;
using AcAuthNetSample.Core.Infrastructure.Repositories.Auth.Interfaces;
using System.Threading.Tasks;

namespace AcAuthNetSample.Core.Application.Auth.Services {
    public class AuthService : IAuthService {

        private readonly IUserRegistractionService _userRegistractionService;
        private readonly IUserRepository _userRespository;
        private readonly IAccessTokenRepository _accessTokenRepository;
        private readonly IAuthTokenService _authTokenService;
        private readonly IPasswordHasher _passwordHasher;


        public AuthService(IUserRegistractionService userRegistractionService,
            IUserRepository userRespository,
            IAuthTokenService authTokenService,
            IPasswordHasher passwordHasher,
            IAccessTokenRepository accessTokenRepository)
        {
            _userRegistractionService = userRegistractionService;
            _userRespository = userRespository;
            _authTokenService = authTokenService;
            _passwordHasher = passwordHasher;
            _accessTokenRepository = accessTokenRepository;
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
                                };

            try
            {
                if(loginToken.FailCount > 5 && loginToken.ExpireTime > DateTime.UtcNow)
                {
                    throw new ArgumentException($"账户已被锁定，请在 {loginToken.ExpireTime.AddHours(8).ToString("yyyy-MM-ddTHH:mm:ss")} 后再次尝试！");
                }

                var validPwd = _passwordHasher.VerifyPassword(password, user.Password!, user.Salt!);
                if (!validPwd)
                {
                    loginToken.FailCount += 1;
                    if(loginToken.FailCount > 5)
                    {
                        loginToken.ExpireTime = DateTime.UtcNow.AddMinutes(30);
                    }
                    throw new ArgumentException("账号或密码错误！");
                }
                loginToken.FailCount = 0;
            }
            finally
            {
                await _accessTokenRepository.CreateOrUpdateTokenAsync(loginToken);
            }
        }
    }
}
