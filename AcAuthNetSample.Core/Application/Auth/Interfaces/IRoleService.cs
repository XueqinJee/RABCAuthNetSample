using AcAuthNetSample.Core.Application.Auth.Dtos;
using AcAuthNetSample.Core.Comments.Dtos;
using AcAuthNetSample.Core.Domain.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Interfaces {
    public interface IRoleService {

        Task<List<RoleDto>> GetRoleList(string? keyword);

        Task<PageResult<RoleDto>> GetRolePageAsync(RoleSearchPageQuery query);

        Task<bool> AddRoleAsync(RoleAddOrUpdateDto role);

        Task<bool> UpdateRoleAsync(RoleAddOrUpdateDto role);

        Task<bool> DeleteRoleAsync(int id);
    }
}
