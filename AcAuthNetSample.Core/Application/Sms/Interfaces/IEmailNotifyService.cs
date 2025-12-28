using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Sms.Interfaces {
    public interface IEmailNotifyService {
        Task<bool> SendCodeToEmailAsync(string email);
    }
}
