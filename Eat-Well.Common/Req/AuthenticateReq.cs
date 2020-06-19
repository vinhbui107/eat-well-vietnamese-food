using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eat_Well.Common.Req
{
    public class AuthenticateReq
    {
        public string username { get; set; }

        public string password { get; set; }
    }
}
