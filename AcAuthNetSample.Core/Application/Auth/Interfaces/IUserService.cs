using AcAuthNetSample.Core.Application.Auth.Dtos;
using AcAuthNetSample.Core.Comments.Dtos;
using AcAuthNetSample.Core.Domain.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Interfaces {
    public interface IUserService {
        Task<PageResult<UserDto>> GetUserPageAsync(UserMrgSearchPageQuery query);

        Task<bool> UpdateUserAsync(UserAddOrUpdateDto user);

        Task<bool> AddUserAsync(UserAddOrUpdateDto user);

        Task<bool> DeleteUserAsync(int id);

        Task<bool> UpdateUserPassword(int userId ,string password, string repassowrd);
    }
}
