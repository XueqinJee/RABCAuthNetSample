using AcAuthNetSample.Core.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class UserDto : BaseEntity{
        public string? UserName { get; set; }
        public string? NickName { get; set; }
        public string? Avator { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
    }
}
