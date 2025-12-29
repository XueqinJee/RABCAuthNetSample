using AcAuthNetSample.Core.Domain.Auth.Entities;
using AcAuthNetSample.Core.Domain.Auth.Interfaces;
using AcAuthNetSample.Core.Infrastructure.Repositories.Auth.Interfaces;

namespace AcAuthNetSample.Core.Domain.Auth.Services {
    public class UserRegistrationService : IUserRegistractionService {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserRegistrationService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _userRepository.IsEmailExistsAsync(email);
        }

        public async Task<bool> IsUserNameExistsAsync(string uerName)
        {
            return await _userRepository.IsUserNameExistsAsync(uerName);
        }

        public async Task<User> RegisterAsync(string nickName, string userName, string email, string password)
        {
            if(await _userRepository.IsUserNameExistsAsync(userName))
            {
                throw new ArgumentNullException("用户名已存在");
            }

            if (await _userRepository.IsEmailExistsAsync(email))
                throw new ArgumentNullException("邮箱已被注册");

            var user = User.Create(userName, email);
            user.NickName = nickName;

            var (salt, hasPwd) = _passwordHasher.HashPassword(password);
            user.SetPassword(salt, hasPwd);

            // 添加到仓储
            await _userRepository.AddAsync(user);
            return user;
        }
    }
}
