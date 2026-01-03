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
    public class UserController : ControllerBase {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("pages")]
        public async Task<IActionResult> Get([FromQuery]UserMrgSearchPageQuery query) { 
            var data = await _userService.GetUserPageAsync(query);
            return JsonResultHelper.Success(data);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody]UserAddOrUpdateDto model)
        {
            var result = await _userService.AddUserAsync(model);
            return JsonResultHelper.Success(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserAddOrUpdateDto model)
        {
            var result = await _userService.UpdateUserAsync(model);
            return JsonResultHelper.Success(result);
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return JsonResultHelper.Success(result);
        }

        [HttpPost("update/passwd")]
        public async Task<IActionResult> UpdatePassword([FromBody] RePasswordDto dto)
        {
            var result = await _userService.UpdateUserPassword(dto.UserId, dto.Password!, dto.Repassword!);
            return JsonResultHelper.Success(result);
        }
    }
}
