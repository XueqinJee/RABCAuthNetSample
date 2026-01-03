using AcAuthNetSample.Core.Application.Auth.Dtos;
using AcAuthNetSample.Core.Application.Auth.Interfaces;
using AcAuthNetSample.Core.Comments.Dtos;
using AcAuthNetSample.Core.Domain.Auth.Entities;
using AcAuthNetSample.Core.Domain.Auth.Interfaces;
using AcAuthNetSample.Core.Infrastructure.Repositories;

namespace AcAuthNetSample.Core.Application.Auth.Services {
    public class UserService : IUserService {
        private readonly IUserManagerService _userManagerService;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserManagerService userManagerService, IPasswordHasher passwordHasher)
        {
            _userManagerService = userManagerService;
            _passwordHasher = passwordHasher;
        }

        public async Task<PageResult<UserDto>> GetUserPageAsync(UserMrgSearchPageQuery query)
        {
            var querable = _userManagerService.GetQueryable();
            if (!string.IsNullOrEmpty(query.UserName))
            {
                querable = querable.Where(x => x.UserName!.Contains(query.UserName));
            }
            if (!string.IsNullOrEmpty(query.NickName))
            {
                querable = querable.Where(x => x.NickName!.Contains(query.NickName));
            }
            var map = querable.Select(x => new UserDto
            {
                Id = x.Id,
                UserName = x.UserName,
                NickName = x.NickName,
                Avator = x.Avator,
                Email = x.Email,
                Phone = x.Phone,
                Description = x.Description,
            });
            return await map.ToPageResultAsync(query);
        }

        public async Task<bool> AddUserAsync(UserAddOrUpdateDto user)
        {
            var usr = User.Create(user.UserName!, user.Email!);
            usr.NickName = user.NickName;

            // 密码
            var (salt, password) = _passwordHasher.HashPassword(user.Password!);
            usr.SetPassword(salt, password);
            return await _userManagerService.AddAsync(usr);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
           return await _userManagerService.DeletePhysicalBySqlAsync(id);
        }

        

        public async Task<bool> UpdateUserAsync(UserAddOrUpdateDto user)
        {
            var usr = await _userManagerService.GetByIdAsync((int)user.Id!);
            if(usr == null)
            {
                throw new Exception("此用户不存在，修改失败");
            }
            usr.NickName = user.NickName;
            usr.Email = user.Email;
            
            return await _userManagerService.UpdateAsync(usr);
        }

        public async Task<bool> UpdateUserPassword(int userId, string password, string repassowrd)
        {
            if(password != repassowrd)
            {
                throw new Exception("密码错误");
            }
            var usr = await _userManagerService.GetByIdAsync(userId);
            if (usr == null)
            {
                throw new Exception("此用户不存在");
            }

            var (salt, hasPwd) = _passwordHasher.HashPassword(password);
            usr.SetPassword(salt, hasPwd);
            return await _userManagerService.UpdateAsync(usr);
        }
    }
}
