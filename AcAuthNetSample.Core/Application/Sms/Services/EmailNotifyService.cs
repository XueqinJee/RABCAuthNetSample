using AcAuthNetSample.Core.Application.Configuration.Options;
using AcAuthNetSample.Core.Application.Sms.Dtos;
using AcAuthNetSample.Core.Application.Sms.Interfaces;
using AcAuthNetSample.Core.Comments.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Security.Cryptography;

namespace AcAuthNetSample.Core.Application.Sms.Services {
    public class EmailNotifyService : IEmailNotifyService {

        private readonly IOptionsSnapshot<EmailSettingOptions> _emailSettingSnapshot;
        private readonly DistributeHelperService _distributeHelperService;
        private readonly ILogger<EmailNotifyService> _logger;

        public EmailNotifyService(
            IOptionsSnapshot<EmailSettingOptions> emailSettingSnapshot,
            ILogger<EmailNotifyService> logger,
            DistributeHelperService distributeHelperService)
        {
            _emailSettingSnapshot = emailSettingSnapshot;
            _logger = logger;
            _distributeHelperService = distributeHelperService;
        }

        public async Task<bool> SendCodeToEmailAsync(string email)
        {
            var config = _emailSettingSnapshot.Value;

            // 验证是否已发送验证码
            var emailCode = _distributeHelperService.Get<EmailCodeDto>(email);
            if (emailCode != null)
            {
                if (emailCode.CreateOn.AddMinutes(1) > DateTime.UtcNow)
                {
                    throw new Exception("邮箱已发送，请稍后重试");
                }
            }

            var code = GenerateNumericCode();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.SenderName, config.SmtpUser));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = $"【{config.SenderName}】验证码通知";
            message.Body = new TextPart("html")
            {
                Text = $@"
                        <!DOCTYPE html>
                        <html>
                        <head><meta charset='UTF-8'></head>
                        <body>
                            <div style='max-width:600px;margin:0 auto;padding:20px;font-family:Arial,sans-serif;'>
                                <h2 style='color:#333;'>{config.SenderName} 验证码</h2>
                                <p>您好！您的验证码是：</p>
                                <div style='font-size:24px;font-weight:bold;color:#0078d4;margin:20px 0;'>{code}</div>
                                <p>该验证码有效期为 {config.VerifyCodeExpireMinutes} 分钟，请及时使用。</p>
                                <p>如非本人操作，请忽略此邮件。</p>
                            </div>
                        </body>
                        </html>"
            };
            using var client = new SmtpClient();
            client.Connect(config.SmtpServer, config.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(config.SmtpUser, config.SmtpPassword);
            await client.SendAsync(message);
            client.Disconnect(true);

            // 放入缓存
            _distributeHelperService.Set(email, new EmailCodeDto
            {
                Code = code,
                Key = email,
                CreateOn = DateTime.UtcNow,
                ExpiresIn = 60 * 5
            }, TimeSpan.FromMinutes(5));
            return true;
        }


        // 生成6位数字验证码
        public string GenerateNumericCode(int length = 6)
        {
            if (length < 4 || length > 8)
                throw new ArgumentException("验证码长度需在4-8位之间");

            var random = RandomNumberGenerator.GetInt32(0, (int)Math.Pow(10, length));
            return random.ToString().PadLeft(length, '0');
        }
    }
}
