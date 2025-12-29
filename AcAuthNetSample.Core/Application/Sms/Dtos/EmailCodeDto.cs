using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Sms.Dtos {
    public class EmailCodeDto {
        public string? Key { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.UtcNow;
        public string? Code { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public long ExpiresIn { get; set; }
    }
}
