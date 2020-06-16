using System;
using System.Collections.Generic;
using System.Text;

namespace Eat_Well.Common.Req
{
    public class UsersReq
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string full_name { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public bool is_admin { get; set; }
        public bool is_active { get; set; }
    }
}
