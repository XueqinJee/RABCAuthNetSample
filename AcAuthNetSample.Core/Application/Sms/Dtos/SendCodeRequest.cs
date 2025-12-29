using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AcAuthNetSample.Core.Application.Sms.Dtos {
    public class SendCodeRequest {

        [Required]
        public string? Email { get; set; }
    }
}
