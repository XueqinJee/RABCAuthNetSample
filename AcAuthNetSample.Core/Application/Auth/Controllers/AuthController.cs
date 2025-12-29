using AcAuthNetSample.Core.Application.Auth.Dtos;
using AcAuthNetSample.Core.Application.Auth.Interfaces;
using AcAuthNetSample.Core.Application.Sms.Dtos;
using AcAuthNetSample.Core.Comments;
using AcAuthNetSample.Core.Comments.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AcAuthNetSample.Core.Application.Auth.Controllers {

    [ApiController]
    [Route("api/{Controller}")]
    public class AuthController : ControllerBase {

        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly DistributeHelperService _distributeHelperService;

        public AuthController(IAuthService authService, ILogger<AuthController> logger, DistributeHelperService distributeHelperService)
        {
            _authService = authService;
            _logger = logger;
            _distributeHelperService = distributeHelperService;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginRequest)
        {

            var userAgent = Request.Headers["User-Agent"].ToString();
            var client = Request.Headers["Client"].FirstOrDefault() ?? "Web";
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            var login = await _authService.LoginAsync(loginRequest.UserName!, loginRequest.Password!, userAgent, client, ip);
            return JsonResultHelper.Success(login);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var _email_cache = _distributeHelperService.Get<EmailCodeDto>(request.Email!);

            if (_email_cache == null || _email_cache.Code != request.Code) 
                return JsonResultHelper.ParamError("验证码错误");
            if (_email_cache.CreateOn.AddSeconds(_email_cache.ExpiresIn) < DateTime.UtcNow) 
                return JsonResultHelper.ParamError("验证码已过期");
            if (request.Password != request.ConfirmPassword) 
                return JsonResultHelper.ParamError("密码和确认密码不一致");

            var result = await _authService.RegisterUserAsync(request);
            return new JsonResult(result);
        }
    }
}
