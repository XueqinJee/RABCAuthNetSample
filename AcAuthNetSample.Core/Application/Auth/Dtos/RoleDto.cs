using AcAuthNetSample.Core.Domain.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class RoleDto : BaseEntity {
        public string? RoleName { get; set; }
        public string? RoleCode { get; set; }
        public string? Description { get; set; }
        public bool IsSystem { get; set; }
        public bool IsEnabled { get; set; }
        public int Sort { get; set; }
    }
}
