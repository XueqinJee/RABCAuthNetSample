using AcAuthNetSample.Core.Domain.Auth.Entities;
using AcAuthNetSample.Core.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.Interfaces {
    public interface IUserManagerService : IBaseRepository<User> {
    }
}
