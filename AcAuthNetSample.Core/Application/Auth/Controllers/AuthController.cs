using AcAuthNetSample.Core.Application.Auth.Dtos;
using AcAuthNetSample.Core.Application.Auth.Interfaces;
using AcAuthNetSample.Core.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcAuthNetSample.Core.Application.Auth.Controllers {

    [ApiController]
    [Route("api/{Controller}")]
    public class AuthController : ControllerBase{

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginRequest) {

            var userAgent = Request.Headers["User-Agent"].ToString();
            var client = Request.Headers["Client"].FirstOrDefault() ?? "Web";
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            var login = await _authService.LoginAsync(loginRequest.UserName!, loginRequest.Password!, userAgent, client, ip);
            return JsonResultHelper.Success(login);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserRequest request)
        {
            var result = await _authService.RegisterUserAsync(request);
            return new JsonResult(result);
        }
    }
}
