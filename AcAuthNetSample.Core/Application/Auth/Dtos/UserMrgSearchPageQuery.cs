using AcAuthNetSample.Core.Comments.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcAuthNetSample.Core.Application.Auth.Dtos {
    public class UserMrgSearchPageQuery : BasePageQuery{
        public string? UserName { get; set; }
        public string? NickName { get; set; }
    }
}
