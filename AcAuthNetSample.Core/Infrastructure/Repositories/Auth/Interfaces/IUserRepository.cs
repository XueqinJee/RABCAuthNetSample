using AcAuthNetSample.Core.Domain.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Infrastructure.Repositories.Auth.Interfaces {
    public interface IUserRepository : IBaseRepository<User> {
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> IsUserNameExistsAsync(string userName);
        Task<bool> IsEmailExistsAsync(string email);
        Task DeleteAsync(User user);
    }
}
