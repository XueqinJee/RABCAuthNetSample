using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Configuration.Options {
    public class EmailSettingOptions {
        public static readonly string Name = "Email";

        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public int VerifyCodeExpireMinutes { get; set; }
        public int VerifyCodeLength { get; set; }
    }
}
