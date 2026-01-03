using AcAuthNetSample.Core.Application.Auth.Dtos;
using AcAuthNetSample.Core.Application.Auth.Interfaces;
using AcAuthNetSample.Core.Comments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Controllers {

    [ApiController]
    [Route("api/{Controller}")]
    public class RoleController : ControllerBase {

        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList(string? keyword)
        {
            var data = await _roleService.GetRoleList(keyword);
            return JsonResultHelper.Success(data);
        }

        [HttpGet("pages")]
        public async Task<IActionResult> Get([FromQuery]RoleSearchPageQuery query)
        {
            var data = await _roleService.GetRolePageAsync(query);
            return JsonResultHelper.Success(data);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRole([FromBody]RoleAddOrUpdateDto model)
        {
            var result = await _roleService.AddRoleAsync(model);
            return JsonResultHelper.Success(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleAddOrUpdateDto model)
        {
            var result = await _roleService.UpdateRoleAsync(model);
            return JsonResultHelper.Success(result);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            return JsonResultHelper.Success(result);
        }
    }
}
