using AcAuthNetSample.Core.Application.Auth.Dtos;
using AcAuthNetSample.Core.Application.Auth.Interfaces;
using AcAuthNetSample.Core.Comments.Dtos;
using AcAuthNetSample.Core.Domain.Auth.Entities;
using AcAuthNetSample.Core.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Services {
    public class RoleService : IRoleService {
        private readonly IBaseRepository<Role> _roleRepository;

        public RoleService(IBaseRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleDto>> GetRoleList(string? keyword)
        {
            var querable = _roleRepository.GetQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                querable = querable.Where(x => x.RoleName.Contains(keyword) || x.RoleCode.Contains(keyword));
            }
            return await querable.Select(x => new RoleDto
            {
                Id = x.Id,
                RoleName = x.RoleName,
                RoleCode = x.RoleCode,
                Description = x.Description,
                IsSystem = x.IsSystem,
                IsEnabled = x.IsEnabled,
                Sort = x.Sort,
                CreateOn = x.CreateOn,
                UpdateOn = x.UpdateOn
            }).ToListAsync();
        }

        public async Task<PageResult<RoleDto>> GetRolePageAsync(RoleSearchPageQuery query)
        {
            var querable = _roleRepository.GetQueryable();
            if (!string.IsNullOrEmpty(query.RoleName))
            {
                querable = querable.Where(x => x.RoleName!.Contains(query.RoleName));
            }
            if (!string.IsNullOrEmpty(query.RoleCode))
            {
                querable = querable.Where(x => x.RoleCode!.Contains(query.RoleCode));
            }
            var map = querable.Select(x => new RoleDto
            {
                Id = x.Id,
                RoleName = x.RoleName,
                RoleCode = x.RoleCode,
                Description = x.Description,
                IsSystem = x.IsSystem,
                IsEnabled = x.IsEnabled,
                Sort = x.Sort,
                CreateOn = x.CreateOn,
                UpdateOn = x.UpdateOn
            });
            return await map.ToPageResultAsync(query);
        }

        public async Task<bool> AddRoleAsync(RoleAddOrUpdateDto role)
        {
            var roleEntity = new Role
            {
                RoleName = role.RoleName,
                RoleCode = role.RoleCode,
                Description = role.Description,
                IsEnabled = role.IsEnabled,
                Sort = role.Sort,
                IsSystem = false,
                CreateOn = DateTime.UtcNow,
                UpdateOn = DateTime.UtcNow
            };
            return await _roleRepository.AddAsync(roleEntity);
        }

        public async Task<bool> UpdateRoleAsync(RoleAddOrUpdateDto role)
        {
            var roleEntity = await _roleRepository.GetByIdAsync((int)role.Id!);
            if (roleEntity == null)
            {
                throw new Exception("此角色不存在，修改失败");
            }

            if (roleEntity.IsSystem && roleEntity.RoleCode != role.RoleCode)
            {
                throw new Exception("系统内置角色不能修改角色编码");
            }

            roleEntity.RoleName = role.RoleName;
            roleEntity.Description = role.Description;
            roleEntity.IsEnabled = role.IsEnabled;
            roleEntity.Sort = role.Sort;
            roleEntity.UpdateOn = DateTime.UtcNow;

            return await _roleRepository.UpdateAsync(roleEntity);
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var roleEntity = await _roleRepository.GetByIdAsync(id);
            if (roleEntity == null)
            {
                throw new Exception("此角色不存在");
            }

            if (roleEntity.IsSystem)
            {
                throw new Exception("系统内置角色不能删除");
            }

            return await _roleRepository.DeletePhysicalBySqlAsync(id);
        }


    }
}
