using AcAuthNetSample.Core.Domain.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.Interfaces {
    public interface IUserRegistractionService {
        Task<User> RegisterAsync(string nickName ,string userName, string email, string password);

        Task<bool> IsEmailExistsAsync(string email);

        Task<bool> IsUserNameExistsAsync(string uerName);
    }
}
