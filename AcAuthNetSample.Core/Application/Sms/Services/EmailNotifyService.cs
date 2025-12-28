using AcAuthNetSample.Core.Application.Configuration.Options;
using AcAuthNetSample.Core.Application.Sms.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace AcAuthNetSample.Core.Application.Sms.Services {
    public class EmailNotifyService : IEmailNotifyService {

        private readonly IOptionsSnapshot<EmailSettingOptions> _emailSettingSnapshot;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<EmailNotifyService> _logger;

        public EmailNotifyService(IMemoryCache memoryCache, 
            IOptionsSnapshot<EmailSettingOptions> emailSettingSnapshot, 
            ILogger<EmailNotifyService> logger)
        {
            _memoryCache = memoryCache;
            _emailSettingSnapshot = emailSettingSnapshot;
            _logger = logger;
        }

        public async Task<bool> SendCodeToEmailAsync(string email)
        {
            var config = _emailSettingSnapshot.Value;
            try
            {
                var code = GenerateNumericCode();
                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(config.SmtpUser, config.SenderName),
                    Subject = $"【{config.SenderName}】验证码通知",
                    Body = $@"
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
                        </html>",
                    IsBodyHtml = true,
                    SubjectEncoding = Encoding.UTF8,
                };
                mailMessage.To.Add(new MailAddress(email));

                // 客户端配置
                using var smtpClient = new SmtpClient(config.SmtpServer)
                {
                    Port = config.SmtpPort,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(
                        config.SmtpUser,
                        config.SmtpPassword),
                    Timeout = 5000
                };
                // 发送邮件
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (SmtpException ex)
            {
                _logger.LogError($"SMTP发送失败：{ex.StatusCode} - {ex.Message}");
                return false;
            }
            catch (Exception ex) {
                _logger.LogError($"发送验证码邮件失败：{ex.Message}");
                return false;
            }
        }


        // 生成6位数字验证码
        public string GenerateNumericCode(int length = 6)
        {
            if (length < 4 || length > 8)
                throw new ArgumentException("验证码长度需在4-8位之间");

            var random = RandomNumberGenerator.GetInt32(0, (int)Math.Pow(10, length));
            return random.ToString().PadLeft(length, '0');
        }

        // 验证验证码（含有效期）
        public bool ValidateCode(string storedCode, string inputCode, int expireMinutes)
        {
            if (string.IsNullOrWhiteSpace(storedCode) || string.IsNullOrWhiteSpace(inputCode))
                return false;

            var parts = storedCode.Split('|');
            if (parts.Length != 2 || !long.TryParse(parts[1], out var timestamp))
                return false;

            var generateTime = DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;
            return parts[0].Equals(inputCode, StringComparison.Ordinal)
                   && DateTime.Now <= generateTime.AddMinutes(expireMinutes);
        }

        // 封装验证码+时间戳
        public string EncodeCode(string code)
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            return $"{code}|{timestamp}";
        }
    }
}
