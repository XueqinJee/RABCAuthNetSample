using AcAuthNetSample.Core.Comments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class RoleSearchPageQuery : BasePageQuery {
        public string? RoleName { get; set; }
        public string? RoleCode { get; set; }
    }
}
