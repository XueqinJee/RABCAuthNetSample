using AcAuthNetSample.Core.Application.Sms.Interfaces;
using AcAuthNetSample.Core.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Sms.Controllers {


    [ApiController]
    [Route("api/{controller}")]
    public class SmsController : ControllerBase {
    
        private readonly ILogger<SmsController> logger;
        private readonly IEmailNotifyService _emailNotifyService;

        public SmsController(IEmailNotifyService emailNotifyService, ILogger<SmsController> logger)
        {
            _emailNotifyService = emailNotifyService;
            this.logger = logger;
        }

        [HttpPost("sendcode")]
        public async Task<IActionResult> SendCode()
        {
            var result = await _emailNotifyService.SendCodeToEmailAsync("2107885241@qq.com");
            if (result)
            {
                return JsonResultHelper.Success("邮箱发送成功！");
            }
            return JsonResultHelper.Warning("发送失败");
        }
    }
}
