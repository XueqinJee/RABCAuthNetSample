using AcAuthNetSample.Core.Comments;
using AcAuthNetSample.Core.Domain.Auth.Entities;
using AcAuthNetSample.Core.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Controllers {

    [ApiController]
    [Route("api/{Controller}")]
    public class RoleController : BaseController<Role> {
        public RoleController(IBaseRepository<Role> repository) : base(repository)
        {
        }
    }
}
