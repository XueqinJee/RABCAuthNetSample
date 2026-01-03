using AcAuthNetSample.Core.Domain.Auth.Entities;
using AcAuthNetSample.Core.Domain.Auth.Interfaces;
using AcAuthNetSample.Core.Infrastructure.Data;
using AcAuthNetSample.Core.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Auth.Services {
    public class UserManagerService : BaseRepository<User>, IUserManagerService {

        private readonly ApplicationDbContext _context;

        public UserManagerService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
